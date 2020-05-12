<template>
  <div class="container-xl">
    <h1>Eu preciso de ajuda</h1>
    <p>Selecione um horário para o agendamento da consulta.</p>
    <calendar-paciente></calendar-paciente>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CalendarPaciente from '@/components/calendar-paciente/calendar-paciente.vue';

@Component({
  components: {
    CalendarPaciente,
  },
})
export default class SouPaciente extends Vue {}
</script>
