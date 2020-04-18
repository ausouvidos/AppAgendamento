<template>
  <div id="app" class="wrapper">
    <header class="header">
      <div class="header-inner container-xl">
        <router-link class="logo" :to="{ name: 'home' }">
          <img
            srcset="./assets/logo.png,
                    ./assets/logo@2x.png 2x,
                    ./assets/logo@3x.png 3x"
            src="./assets/logo.png"
            alt="Projeto Aus Ouvidos">
        </router-link>
        <nav class="header-nav">
          <router-link :to="{ name: 'minha-agenda' }" class="btn btn-outline-primary">Sou psicólogo</router-link>
          <router-link :to="{ name: 'agendar-consulta' }" class="btn btn-primary">Sou paciente</router-link>
        </nav>
      </div>
    </header>
    <section class="main-content container-xl">
      <router-view />
    </section>
    <footer class="footer">
      <div class="container-xl">
        &copy; Copyright Aus Ouvidos, 2020. Todos os direitos reservados.
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
