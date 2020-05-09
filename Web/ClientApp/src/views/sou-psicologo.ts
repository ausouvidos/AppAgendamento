import { Component, Vue } from 'vue-property-decorator';
import { ModalPlugin } from 'bootstrap-vue';
import CalendarPsicologo from '@/components/calendar-psicologo/calendar-psicologo.vue';

Vue.use(ModalPlugin);

@Component({
  components: {
    CalendarPsicologo,
  },
})
export default class SouPsicologo extends Vue {
  private isLoggedIn: boolean | null = null;

  private mounted() {
    this.isLoggedIn = !!sessionStorage.getItem('msal.idtoken');
  }

  private signIn() {
    const script = document.createElement('script');
    script.src = 'https://alcdn.msftauth.net/lib/1.2.1/js/msal.js';
    script.onload = () => {
      this.openSignInPopup();
    };
    document.head.appendChild(script);
  }

  private async openSignInPopup() {
    try {
      const msalConfig = {
        auth: {
          clientId: '4c6c3ac0-a8f7-42f9-8b8e-60b7d829b42d',
          authority: 'https://login.microsoftonline.com/5560e420-5f71-400c-8bbe-e52fae72eb6c',
          redirectUri: new URL(window.location.href).origin + '/signin-oidc',
        },
        cache: {
          cacheLocation: 'sessionStorage',
          storeAuthStateInCookie: false,
        },
      };
      const myMSALObj = new Msal.UserAgentApplication(msalConfig);
      const response = await myMSALObj.loginPopup({ scopes: ['openid'] });
      this.isLoggedIn = true;
    } catch (error) {
      console.error(error);
    }
  }

  private onUnauthorized() {
    this.isLoggedIn = false;
    sessionStorage.removeItem('msal.idtoken');
    (this.$refs['session-expired-modal'] as any).show();
  }
}
