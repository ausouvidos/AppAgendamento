import { Component, Vue } from 'vue-property-decorator';
import FullCalendar from '@fullcalendar/vue';
import timeGridPlugin from '@fullcalendar/timegrid';
import ptbrLocale from '@fullcalendar/core/locales/pt-br';
import availabilityService from '@/services/availability.service';
import AvailabilityDate from '@/models/availability-date.model';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';

@Component({
  components: {
    FullCalendar,
  },
})
export default class CalendarPaciente extends Vue {
  private calendarPlugins = [timeGridPlugin];
  private calendarLocale = ptbrLocale;
  private availableSpots: AvailabilityDate[] = [];
  private reservationName = '';
  private reservationEmail = '';
  private reservationStart = new Date();
  private reservationEnd = new Date();
  private isLoading = false;

  private mounted() {
    this.reservationName = sessionStorage.getItem('reservationName') || '';
    this.reservationEmail = sessionStorage.getItem('reservationEmail') || '';
    this.fetchData();
  }

  private async fetchData() {
    this.availableSpots = await availabilityService.getWeeklyAvailableSpots();
  }

  private onEventClick(data: any) {
    this.reservationStart = data.event.start;
    this.reservationEnd = data.event.end;
    (this.$refs['reservation-modal'] as any).show();
  }

  private showConfirmation() {
    (this.$refs['confirmation-modal'] as any).show();
  }

  private closeModal() {
    (this.$refs['reservation-modal'] as any).hide();
  }

  private async reserveSpot(bvModalEvt: any) {
    bvModalEvt.preventDefault();

    if (this.isLoading || !this.reservationName || !this.reservationEmail) {
      return;
    }

    this.isLoading = true;

    try {
      const data = new ReserveSpotRequest({
        name: this.reservationName,
        email: this.reservationEmail,
        start: this.reservationStart,
        end: this.reservationEnd,
      });
      const response = await availabilityService.reserveSpot(data);
      if (response) {
        sessionStorage.setItem('reservationName', this.reservationName);
        sessionStorage.setItem('reservationEmail', this.reservationEmail);
        this.closeModal();
        this.showConfirmation();
        this.fetchData();
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
