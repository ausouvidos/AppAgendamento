import { Component, Vue } from 'vue-property-decorator';
import availabilityService from '@/services/availability.service';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import SpotSchedule from '@/models/spot-schedule.model';
import AvailabilityDate from '@/models/availability-date.model';

@Component
export default class CalendarPaciente extends Vue {
  private schedule = new SpotSchedule();
  private reservation = new ReserveSpotRequest();
  private isLoading = false;
  private isScheduleLoading = false;

  private mounted() {
    this.fetchData();
  }

  private fetchData() {
    this.schedule.load();
  }

  private async changeWeek(prevOrNext = 1) {
    if (this.isScheduleLoading) {
      return;
    }

    this.isScheduleLoading = true;

    try {
      if (prevOrNext >= 0) {
        await this.schedule.nextWeek();
      } else {
        await this.schedule.previousWeek();
      }
    } catch (error) {
      console.error(error);
    } finally {
      this.isScheduleLoading = false;
    }
  }

  private async reserveSpot(bvModalEvt: any) {
    bvModalEvt.preventDefault();

    if (this.isLoading || !this.reservation.isValid()) {
      return;
    }

    this.isLoading = true;

    try {
      const response = await availabilityService.reserveSpot(this.reservation);
      if (response) {
        this.hideReservationModal();
        this.showConfirmationModal();
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

  private showReservationModal(spot: AvailabilityDate) {
    this.reservation.start = spot.start;
    this.reservation.end = spot.end;
    (this.$refs['reservation-modal'] as any).show();
  }

  private hideReservationModal() {
    (this.$refs['reservation-modal'] as any).hide();
  }

  private showConfirmationModal() {
    (this.$refs['confirmation-modal'] as any).show();
  }
}
