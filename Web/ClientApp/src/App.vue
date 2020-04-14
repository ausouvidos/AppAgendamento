<template>
  <div id="app">
    <div class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom shadow-sm">
      <h3 class="my-0 mr-md-auto font-weight-normal">
        <router-link :to="{ name: 'home' }" class="text-body text-decoration-none">Aus Ouvidos</router-link>
      </h3>
      <nav class="mt-2 mt-md-0">
        <router-link :to="{ name: 'agendar-consulta' }" class="m-2 btn btn-primary">Agendar consulta</router-link>
        <router-link :to="{ name: 'minha-agenda' }" class="m-2 btn btn-outline-primary">Sou psicólogo</router-link>
      </nav>
    </div>
    <div class="p-3 mb-5 px-md-4">
      <router-view />
    </div>
  </div>
</template>

<style src="./styles/main.scss" lang="scss"></style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class App extends Vue {}
</script>
