import { Component, Vue } from 'vue-property-decorator';
import { TheMask } from 'vue-the-mask';
import { ValidationProvider, ValidationObserver } from 'vee-validate';
import '@/utils/vee-validate-config';
import analytics from '@/utils/analytics';
import availabilityService from '@/services/availability.service';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import SpotSchedule from '@/models/spot-schedule.model';
import AvailabilityDate from '@/models/availability-date.model';

@Component({
  components: {
    TheMask,
    ValidationProvider,
    ValidationObserver,
  },
})
export default class CalendarPaciente extends Vue {
  private schedule = new SpotSchedule();
  private reservation = new ReserveSpotRequest();
  private hasFailed = false;
  private isLoading = false;
  private isScheduleLoading = false;
  private recaptchaKey = '6Lc4mPAUAAAAAMHl3isSP6rBQ6xzoDgxMpXvBiXS';
  private recaptchaPromise!: Promise<any>;
  private errorMessage?: string = '';

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
        analytics.sendEvent('paciente', 'navegacao_proxima_semana');
        await this.schedule.nextWeek();
      } else {
        analytics.sendEvent('paciente', 'navegacao_semana_anterior');
        await this.schedule.previousWeek();
      }
    } catch (error) {
      console.error(error);
    } finally {
      this.isScheduleLoading = false;
    }
  }

  private handleOk(bvModalEvt: any) {
    bvModalEvt.preventDefault();
    this.reserveSpot();
  }

  private async reserveSpot(evt?: any) {
    evt?.preventDefault();

    const isValid = await (this.$refs.observer as InstanceType<typeof ValidationObserver>).validate();

    if (this.isLoading || !isValid) {
      return;
    }

    this.isLoading = true;

    try {
      const response = await availabilityService.reserveSpot(this.reservation);
      if (response.succeeded) {
        this.hideReservationModal();
        this.showConfirmationModal();
        this.fetchData();
        analytics.sendEvent('paciente', 'agendamento_concluido');
      } else {
        this.hasFailed = true;
        this.errorMessage = response.message;
      }
    } catch (error) {
      this.hasFailed = true;
    } finally {
      this.isLoading = false;
    }
  }

  private showReservationModal(spot: AvailabilityDate) {
    this.hasFailed = false;
    this.errorMessage = '';
    this.reservation.start = spot.start;
    this.reservation.end = spot.end;
    this.reservation.recaptchaResponse = '';
    (this.$refs['reservation-modal'] as any).show();
    this.renderRecaptcha();
    analytics.sendEvent('paciente', 'agendamento_modal');
  }

  private renderRecaptcha() {
    const callback = (recaptchaResponse?: string) => {
      this.reservation.recaptchaResponse = recaptchaResponse || '';
      (this.$refs.captchaInput as HTMLInputElement).dispatchEvent(new Event('change'));
    };
    this.recaptchaPromise.then(() => {
      grecaptcha.render('recaptcha-container', {
        callback,
        'expired-callback': callback,
        'sitekey': this.recaptchaKey,
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
