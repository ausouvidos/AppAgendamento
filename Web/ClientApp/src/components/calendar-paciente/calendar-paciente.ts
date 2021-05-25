import { Component, Vue } from 'vue-property-decorator';
import { BvModalEvent } from 'bootstrap-vue';
import { TheMask } from 'vue-the-mask';
import { ValidationProvider, ValidationObserver } from 'vee-validate';
import '@/utils/vee-validate-config';
import analytics from '@/utils/analytics';
import availabilityService from '@/services/availability.service';
import companyService from '@/services/company.service';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import SpotSchedule from '@/models/spot-schedule.model';
import AvailabilityDate from '@/models/availability-date.model';
import Company from '@/models/company.model';
import { Step, Steps, Button, Alert } from 'element-ui';

@Component({
  components: {
    TheMask,
    ValidationProvider,
    ValidationObserver,
    'el-step': Step,
    'el-steps': Steps,
    'el-button': Button,
    'el-alert': Alert,
  },
})
export default class CalendarPaciente extends Vue {
  private schedule = new SpotSchedule();
  private reservation = new ReserveSpotRequest();
  private newCompany = new Company();
  private hasFailed = false;
  private isLoading = false;
  private isScheduleLoading = false;
  private recaptchaKey = '6Lc4mPAUAAAAAMHl3isSP6rBQ6xzoDgxMpXvBiXS';
  private recaptchaPromise!: Promise<any>;
  private errorMessage?: string = '';
  private estados = [
    'AC',
    'AL',
    'AP',
    'AM',
    'BA',
    'CE',
    'DF',
    'ES',
    'GO',
    'MA',
    'MT',
    'MS',
    'MG',
    'PA',
    'PB',
    'PR',
    'PE',
    'PI',
    'RJ',
    'RN',
    'RS',
    'RO',
    'RR',
    'SC',
    'SP',
    'SE',
    'TO',
  ];
  private phoneMaskConfig = {
    mode: 'international',
    onlyCountries: ['BR', 'MZ'],
    required: true,
    enabledCountryCode: false,
    defaultCountry: 'BR',
    dropdownOptions: {
      disabledDialCode: false,
    },
    inputOptions: {
      showDialCode: false,
    },
    placeholder: 'telefone',
  };
  private currentStep: number = 0;
  private stepButtonText: string = 'Prosseguir';
  private spotIndex: number = -1;

  private mounted() {
    this.recaptchaPromise = this.loadRecaptcha();
    setTimeout(() => {
      this.renderRecaptcha(
        'recaptcha-container',
        'reservation-captcha',
        this.reservation,
      );
    }, 500);
  }

  private async fetchData() {
    this.isScheduleLoading = true;
    await this.schedule.load(this.reservation.voucher);
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
    this.spotIndex = -1;

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

  private handleOk(bvModalEvt: BvModalEvent) {
    bvModalEvt.preventDefault();
    this.reserveSpot();
  }

  private async reserveSpot(evt?: any) {
    evt?.preventDefault();

    const isValid = await (this.$refs.observer as InstanceType<
      typeof ValidationObserver
    >).validate();

    if (this.isLoading || !isValid) {
      return;
    }

    this.isLoading = true;

    try {
      const response = await availabilityService.reserveSpot(this.reservation);
      if (response.succeeded) {
        this.hideModal('reservation-modal');
        this.showModal('confirmation-modal');
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

  private handleOkCompany(bvModalEvt: BvModalEvent) {
    bvModalEvt.preventDefault();
    this.addCompany();
  }

  private async addCompany(evt?: any) {
    evt?.preventDefault();

    const isValid = await (this.$refs.observerCompany as InstanceType<
      typeof ValidationObserver
    >).validate();

    if (this.isLoading || !isValid) {
      return;
    }

    this.isLoading = true;

    try {
      const response = await companyService.add(this.newCompany);
      if (response.succeeded) {
        this.hideModal('company-modal');
        this.showModal('company-confirmation-modal');
        analytics.sendEvent('paciente', 'instituicao_cadastro');
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

  private resetErrorMessage() {
    this.hasFailed = false;
    this.errorMessage = '';
  }

  private showReservationModal(spot: AvailabilityDate, index: number) {
    this.reservation.start = spot.start;
    this.reservation.end = spot.end;
    this.resetErrorMessage();
    this.spotIndex = index;
  }

  private showCompanyModal() {
    this.newCompany = new Company();
    this.resetErrorMessage();
    this.hideModal('reservation-modal');
    this.showModal('company-modal');
    this.renderRecaptcha(
      'recaptcha-container-company',
      'company-captcha',
      this.newCompany,
    );
    analytics.sendEvent('paciente', 'instituicao_modal');
  }

  private renderRecaptcha(
    containerId: string,
    inputRef: string,
    model: ReserveSpotRequest | Company,
  ) {
    const callback = (recaptchaResponse?: string) => {
      model.recaptchaResponse = recaptchaResponse || '';
      (this.$refs[inputRef] as HTMLInputElement).dispatchEvent(
        new Event('change'),
      );
    };
    this.recaptchaPromise.then(() => {
      grecaptcha.render(containerId, {
        callback,
        'expired-callback': callback,
        'sitekey': this.recaptchaKey,
      });
    });
  }

  private showModal(key: string) {
    (this.$refs[key] as any).show();
  }

  private hideModal(key: string) {
    (this.$refs[key] as any).hide();
  }

  private async validateAndNext() {

      if (this.currentStep === 0) {

          const isValid = await (this.$refs.observer as InstanceType<
              typeof ValidationObserver
          >).validate();
          if (this.isLoading || !isValid || !this.reservation.voucher) {
              return;
          }
          this.isLoading = true;
          try {
              const response = await availabilityService.validateCode(
                  this.reservation.voucher,
              );
              if (response.succeeded) {
                  this.resetErrorMessage();
                  this.currentStep = 1;
                  this.stepButtonText = 'Realizar agendamento';
                  this.fetchData();
                  analytics.sendEvent('paciente', 'agendamento_modal');
              } else {
                  this.hasFailed = true;
                  this.errorMessage = response.message;
              }
          } catch (error) {
              this.hasFailed = true;
          } finally {
              this.isLoading = false;
          }
      } else if (this.currentStep === 1) {
          if (this.isLoading || !this.reservation.start || !this.reservation.end) {
              return;
          }
          this.isLoading = true;
          try {
              const response = await availabilityService.reserveSpot(this.reservation);
              if (response.succeeded) {
                  this.resetErrorMessage();
                  this.currentStep = 2;
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
  }
}
