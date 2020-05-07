<template>
  <div class="container-xl">
    <h1>Página não encontrada</h1>
    <p>Esta página não foi encontrada, mas nós ainda estamos aqui para ouvir você.</p>
    <router-link :to="{ name: 'sou-paciente' }" class="btn btn-primary">Agende um horário</router-link>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class NotFound extends Vue {}
</script>
