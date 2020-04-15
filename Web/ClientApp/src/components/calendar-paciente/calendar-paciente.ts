import { Component, Vue } from 'vue-property-decorator';
import FullCalendar from '@fullcalendar/vue';
import timeGridPlugin from '@fullcalendar/timegrid';
import ptbrLocale from '@fullcalendar/core/locales/pt-br';
import availabilityService from '@/services/availability.service';
import AvailabilityDate from '@/models/availability-date.model';

@Component({
  components: {
    FullCalendar,
  },
})
export default class CalendarPaciente extends Vue {
  private calendarPlugins = [timeGridPlugin];
  private calendarLocale = ptbrLocale;
  private availableSpots: AvailabilityDate[] = [];

  private mounted() {
    this.fetchData();
  }

  private async fetchData() {
    this.availableSpots = await availabilityService.getWeeklyAvailableSpots();
  }
}
