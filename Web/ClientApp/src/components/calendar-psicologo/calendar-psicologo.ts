import { Component, Vue } from 'vue-property-decorator';
import { BvModalEvent } from 'bootstrap-vue';
import moment from 'moment';
import FullCalendar from '@fullcalendar/vue';
import timeGridPlugin from '@fullcalendar/timegrid';
import ptbrLocale from '@fullcalendar/core/locales/pt-br';
import availabilityService from '@/services/availability.service';
import AddAvailabilitiesRequest from '@/models/add-availabilities-request.model';
import CompleteAvailabilityRequest from '@/models/complete-availability-request.model';
import AvailabilityEventUI from '@/models/availability-event-ui.model';

@Component({
  components: {
    FullCalendar,
  },
})
export default class CalendarPsicologo extends Vue {
  private calendarPlugins = [timeGridPlugin];
  private calendarLocale = ptbrLocale;
  private events: AvailabilityEventUI[] = [];
  private selectedEvent: AvailabilityEventUI | null = null;
  private hasFailed = false;
  private errorMessage?: string = '';
  private modalKeys = {
    addAvailability: 'availability-modal',
    futureEvent: 'future-event-modal',
    pastEvent: 'past-event-modal',
  };
  private customButtons = {
    addEvent: {
      text: 'Adicionar horário',
      click: () => this.showModal(this.modalKeys.addAvailability),
    },
  };
  private availabilityDate = moment().format('YYYY-MM-DD');
  private availabilityTimeStart = '';
  private availabilityTimeEnd = '';
  private availableTimes: string[] = [];
  private isLoading = false;
  private minDate = new Date().toISOString().split('T')[0];

  private mounted() {
      this.fetchData();
      this.calculateAvailableTimes();
    }

    private calculateAvailableTimes() {
        if (moment().diff(moment(this.availabilityDate, 'YYYY-MM-DD'), 'day', true) < 0) {
            this.availableTimes = new Array(24).fill(0).map((item, i) => `${i > 9 ? i : '0' + i}:00`);
        } else {
            const m: moment.Moment = moment();
            const roundUp: moment.Moment = m.minute() || m.second() || m.millisecond()
                ? m.add(2, 'hour').startOf('hour')
                : m.startOf('hour');
            const available: string[] = [];
            do {
                available.push(roundUp.format('HH:mm'));
                roundUp.add(1, 'h');
            } while (roundUp.diff(moment().startOf('day').add(1, 'd'), 'day', true) < 0);
            this.availableTimes = available;
        }
    }

  private async fetchData() {
    try {
      const availabilities = await availabilityService.getMyAvailabilities();
      this.events = availabilities.map((item) => new AvailabilityEventUI(item));
    } catch (error) {
      this.handleAPIError(error);
    }
  }

  private resetErrorMessage() {
    this.hasFailed = false;
    this.errorMessage = '';
  }

  private showModal(key: string) {
    this.resetErrorMessage();
    (this.$refs[key] as any).show();
  }

  private hideModal(key: string) {
    (this.$refs[key] as any).hide();
  }

  private async addAvailability(bvModalEvt: BvModalEvent) {
    bvModalEvt.preventDefault();

    if (this.isLoading || !this.availabilityDate || !this.availabilityTimeStart) {
      return;
    }

    this.isLoading = true;

    try {
      const data = new AddAvailabilitiesRequest();
      data.dates = [{
        start: moment(`${this.availabilityDate}T${this.availabilityTimeStart}`).toDate(),
        end: moment(`${this.availabilityDate}T${this.availabilityTimeStart}`).add(1, 'hour').toDate(),
      }];
      const response = await availabilityService.add(data);
      if (response.succeeded) {
        this.fetchData();
        this.hideModal(this.modalKeys.addAvailability);
      } else {
        this.hasFailed = true;
        this.errorMessage = response.message;
      }
    } catch (error) {
      this.hasFailed = true;
      this.handleAPIError(error);
    } finally {
      this.isLoading = false;
    }
  }

  private async eventClick({ event }: any) {
    this.selectedEvent = this.events.find((e) => e.id === parseInt(event.id, 10)) || null;
    if (this.selectedEvent) {
      this.showModal(this.selectedEvent.isFree ? this.modalKeys.futureEvent : this.modalKeys.pastEvent);
    }
  }

  private async removeAvailability(bvModalEvt: BvModalEvent) {
    bvModalEvt.preventDefault();

    if (this.isLoading || !this.selectedEvent || !confirm('Tem certeza que deseja remover este horáro?')) {
      return;
    }

    this.isLoading = true;

    try {
      const response = await availabilityService.remove(this.selectedEvent.id);
      if (response.succeeded) {
        this.fetchData();
        this.hideModal(this.modalKeys.futureEvent);
      } else {
        this.hasFailed = true;
        this.errorMessage = response.message;
      }
    } catch (error) {
      this.hasFailed = true;
      this.handleAPIError(error);
    } finally {
      this.isLoading = false;
    }
  }

  private async completeAvailability(bvModalEvt: BvModalEvent) {
    bvModalEvt.preventDefault();

    if (this.isLoading || !this.selectedEvent) {
      return;
    }

    this.isLoading = true;

    try {
      const data = new CompleteAvailabilityRequest({
        id: this.selectedEvent.id,
        notas: this.selectedEvent.notes,
        observacoes: this.selectedEvent.observations,
      });
      const response = await availabilityService.complete(data);
      if (response.succeeded) {
        this.fetchData();
        this.hideModal(this.modalKeys.pastEvent);
      } else {
        this.hasFailed = true;
        this.errorMessage = response.message;
      }
    } catch (error) {
      this.hasFailed = true;
      this.handleAPIError(error);
    } finally {
      this.isLoading = false;
    }
  }

  private handleAPIError(error: any) {
    if (error.response.status === 401) {
      this.$emit('unauthorizedRequest');
    }
  }
}
