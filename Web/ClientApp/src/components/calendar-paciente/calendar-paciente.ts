import { Component, Vue } from 'vue-property-decorator';
import { TheMask } from 'vue-the-mask';
import availabilityService from '@/services/availability.service';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import SpotSchedule from '@/models/spot-schedule.model';
import AvailabilityDate from '@/models/availability-date.model';

@Component({
  components: {
    TheMask,
  },
})
export default class CalendarPaciente extends Vue {
  private schedule = new SpotSchedule();
  private reservation = new ReserveSpotRequest();
  private hasFailed = false;
  private isLoading = false;
  private isScheduleLoading = false;
  private recaptchaKey = '6LcAFuwUAAAAAABdPF9EYwoy2d2AhRaFAynRckFx';
  private recaptchaPromise!: Promise<any>;

  private mounted() {
    this.fetchData();
    this.recaptchaPromise = this.loadRecaptcha();
  }

  private async fetchData() {
    this.isScheduleLoading = true;
    await this.schedule.load();
    this.isScheduleLoading = false;
  }

  private loadRecaptcha() {
    return new Promise((resolve) => {
      if (window.grecaptcha) {
        resolve();
      } else {
        const script = document.createElement('script');
        script.src = 'https://www.google.com/recaptcha/api.js?render=explicit';
        script.onload = () => {
          resolve();
        };
        document.head.appendChild(script);
      }
    });
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
        this.hasFailed = true;
      }
    } catch (error) {
      this.hasFailed = true;
    } finally {
      this.isLoading = false;
    }
  }

  private showReservationModal(spot: AvailabilityDate) {
    this.hasFailed = false;
    this.reservation.start = spot.start;
    this.reservation.end = spot.end;
    this.reservation.recaptchaResponse = '';
    (this.$refs['reservation-modal'] as any).show();
    this.recaptchaPromise.then(() => {
      grecaptcha.render('recaptcha-container', {
        sitekey : this.recaptchaKey,
        callback : (recaptchaResponse: string) => {
          this.reservation.recaptchaResponse = recaptchaResponse;
        },
      });
    });
  }

  private hideReservationModal() {
    (this.$refs['reservation-modal'] as any).hide();
  }

  private showConfirmationModal() {
    (this.$refs['confirmation-modal'] as any).show();
  }
}
