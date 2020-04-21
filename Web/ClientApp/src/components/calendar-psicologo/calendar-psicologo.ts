import { Component, Vue } from 'vue-property-decorator';
import { ModalPlugin } from 'bootstrap-vue';
import moment from 'moment';
import FullCalendar from '@fullcalendar/vue';
import timeGridPlugin from '@fullcalendar/timegrid';
import ptbrLocale from '@fullcalendar/core/locales/pt-br';
import availabilityService from '@/services/availability.service';
import AddAvailabilitiesRequest from '@/models/add-availabilities-request.model';
import AvailabilityEventUI from '@/models/availability-event-ui.model';

Vue.use(ModalPlugin);

@Component({
  components: {
    FullCalendar,
  },
})
export default class CalendarPsicologo extends Vue {
  private calendarPlugins = [timeGridPlugin];
  private calendarLocale = ptbrLocale;
  private events: AvailabilityEventUI[] = [];
  private modalShow = false;
  private customButtons = {
    addEvent: {
      text: 'Adicionar horário',
      click: () => this.openAvailabilityModal(),
    },
  };
  private availabilityDate = moment().format('YYYY-MM-DD');
  private availabilityTimeStart = '';
  private availabilityTimeEnd = '';
  private availableTimes = new Array(24).fill(0).map((item, i) => `${i > 9 ? i : '0' + i}:00`);
  private isLoading = false;

  private mounted() {
    this.fetchData();
  }

  private async fetchData() {
    const availabilities = await availabilityService.getMyAvailabilities();
    this.events = availabilities.map((item) => new AvailabilityEventUI(item));
  }

  private openAvailabilityModal() {
    (this.$refs['availability-modal'] as any).show();
  }

  private closeModal() {
    (this.$refs['availability-modal'] as any).hide();
  }

  private async addAvailability(bvModalEvt: any) {
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
      if (response) {
        this.fetchData();
        this.closeModal();
      } else {
        console.error('error');
      }
    } catch (error) {
      console.error(error);
    } finally {
      this.isLoading = false;
    }
  }
}
