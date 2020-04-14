<template>
  <div>
    <h1>Agendar consulta</h1>
    <p>Selecione um horário</p>
    <calendar></calendar>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Calendar from '@/components/calendar/calendar.vue';

@Component({
  components: {
    Calendar,
  },
})
export default class AgendarConsulta extends Vue {}
</script>
