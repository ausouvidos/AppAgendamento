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
  private availableTimes = new Array(24).fill(0).map((item, i) => `${i > 9 ? i : '0' + i}:00`);

  private mounted() {
    this.fetchData();
  }

  private async fetchData() {
    this.availabilities = await availabilityService.getMyAvailabilities();
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
      if (response) {
        this.fetchData();
        this.closeAvailabilityModal();
      } else {
        console.error('error');
      }
    } catch (error) {
      console.error(error);
    }
  }
}
