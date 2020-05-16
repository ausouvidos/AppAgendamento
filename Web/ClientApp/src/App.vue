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
          <router-link :to="{ name: 'sou-psicologo' }" class="btn btn-outline-primary">Eu quero escutar</router-link>
          <router-link :to="{ name: 'sou-paciente' }" class="btn btn-primary">Eu quero conversar</router-link>
        </nav>
      </div>
    </header>
    <section class="main-content">
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

@Component
export default class App extends Vue {}
</script>
