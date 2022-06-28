<template>
  <div class="container-xl">
    <h1 style="text-align: center; margin-bottom: 3rem">Eu quero conversar</h1>
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
