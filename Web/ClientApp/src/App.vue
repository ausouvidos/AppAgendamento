<template>
  <div id="app" class="wrapper">
    <header class="header">
      <div class="header-inner container-xl">
        <router-link class="logo" :to="{ name: 'home' }">
          <img
            srcset="./assets/logo-aus-ouvidos.png,
                    ./assets/logo-aus-ouvidos@2x.png 2x,
                    ./assets/logo-aus-ouvidos@3x.png 3x"
            src="./assets/logo-aus-ouvidos.png"
            alt="Projeto Aus Ouvidos">
        </router-link>
        <nav class="header-nav">
          <router-link :to="{ name: 'minha-agenda' }" class="btn btn-outline-primary">Sou psicólogo</router-link>
          <router-link :to="{ name: 'agendar-consulta' }" class="btn btn-primary">Sou paciente</router-link>
        </nav>
      </div>
    </header>
    <banner v-show="isHome"></banner>
    <section class="main-content container-xl">
      <router-view />
    </section>
    <footer class="footer">
      <div class="container-xl">
        &copy; Copyright Aus Ouvidos, 2020. Todos os direitos reservados. | Realização
        <a href="https://www.mtac.org.br/" target="_blank">
          MTAC
          <img
            srcset="./assets/logo-mtac.png,
                    ./assets/logo-mtac@2x.png 2x,
                    ./assets/logo-mtac@3x.png 3x"
            src="./assets/logo-mtac.png"
            alt="MTAC">
        </a>
      </div>
    </footer>
  </div>
</template>

<style src="./styles/main.scss" lang="scss"></style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Banner from '@/components/banner/banner.vue';

@Component({
  components: {
    Banner,
  }
})
export default class App extends Vue {
  get isHome() {
    return this.$route.name === 'home';
  }
}
</script>
