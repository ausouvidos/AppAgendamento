import { Component, Vue } from 'vue-property-decorator';
import { ModalPlugin } from 'bootstrap-vue';
import moment from 'moment';
import FullCalendar from '@fullcalendar/vue';
import timeGridPlugin from '@fullcalendar/timegrid';
import ptbrLocale from '@fullcalendar/core/locales/pt-br';
import availabilityService from '@/services/availability.service';
import Availability from '@/models/availability.model';
import AddAvailabilitiesRequest from '@/models/add-availabilities-request.model';

Vue.use(ModalPlugin);

@Component({
  components: {
    FullCalendar,
  },
})
export default class CalendarPsicologo extends Vue {
  private calendarPlugins = [timeGridPlugin];
  private calendarLocale = ptbrLocale;
  private availabilities: Availability[] = [];
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
  private availableTimes = ['00:00', '01:00'];

  private mounted() {
    this.fetchData();
  }

  private handleDateClick(info: any) {
    console.log(info);
  }

  private async fetchData() {
    this.availabilities = await availabilityService.getMyAvailabilities();
    console.log(this.availabilities);
  }

  private openAvailabilityModal() {
    (this.$refs['my-modal'] as any).show();
  }

  private closeAvailabilityModal() {
    (this.$refs['my-modal'] as any).hide();
  }

  private async addAvailability() {
    try {
      const data = new AddAvailabilitiesRequest();
      data.dates = [{
        start: moment(`${this.availabilityDate}T${this.availabilityTimeStart}`).toDate(),
        end: moment(`${this.availabilityDate}T${this.availabilityTimeEnd}`).toDate(),
      }];
      const response = await availabilityService.add(data);
      console.log(response);
      this.closeAvailabilityModal();
    } catch (error) {
      console.log(error);
    }
  }
}
