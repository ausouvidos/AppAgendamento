<template>
  <div class="container-xl">
    <h1>Sou psicólogo</h1>
    <div v-if="isLoggedIn === true">
      <p>Adicione um horário em sua agenda para o atendimento.</p>
      <calendar-psicologo></calendar-psicologo>
    </div>
    <div v-if="isLoggedIn === false">
      <p>É necessário estar logado para acessar esta página.</p>
      <button class="btn btn-primary" @click="signIn">Fazer login</button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CalendarPsicologo from '@/components/calendar-psicologo/calendar-psicologo.vue';

@Component({
  components: {
    CalendarPsicologo,
  },
})
export default class MinhaAgenda extends Vue {
  private isLoggedIn: boolean | null = null;

  private mounted() {
    this.isLoggedIn = !!sessionStorage.getItem('msal.idtoken');
  }

  private signIn() {
    const script = document.createElement('script');
    script.src = 'https://alcdn.msftauth.net/lib/1.2.1/js/msal.js';
    script.onload = () => {
      this.msalLoaded();
    };
    document.head.appendChild(script);
  }

  private async msalLoaded() {
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
}
</script>
