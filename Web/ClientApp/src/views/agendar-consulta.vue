<template>
  <div>
    <h1>Agendar consulta</h1>
    <p>Selecione um horário</p>
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
export default class AgendarConsulta extends Vue {}
</script>
