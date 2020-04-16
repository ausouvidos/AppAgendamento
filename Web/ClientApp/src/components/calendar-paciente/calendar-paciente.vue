<template>
  <div>
    <full-calendar
      ref="fullCalendar"
      class="calendar-custom calendar-custom-clickable"
      defaultView="timeGridWeek"
      slotDuration="01:00"
      height="auto"
      @eventClick="onEventClick"
      :allDaySlot="false"
      :slotLabelFormat="{
        hour: 'numeric',
        minute: '2-digit',
        omitZeroMinute: false,
        meridiem: 'short'
      }"
      :header="{
        left: 'prev,next today',
        center: 'title',
        right: ''
      }"
      :locale="calendarLocale"
      :plugins="calendarPlugins"
      :events="availableSpots"></full-calendar>

    <b-modal ref="reservation-modal" ok-title="Agendar" cancel-title="Cancelar" @ok="reserveSpot">
      <template v-slot:modal-title>Agendar consulta</template>
      <p>{{ reservationStart | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservationEnd | date('H:mm') }}</p>
      <div class="form-group">
        <label for="reservation-name">Nome</label>
        <input type="text" class="form-control" id="reservation-name" v-model="reservationName">
      </div>
      <div class="form-group">
        <label for="reservation-email">Email</label>
        <input type="email" class="form-control" id="reservation-email" v-model="reservationEmail">
      </div>
    </b-modal>

    <b-modal ref="confirmation-modal" hide-footer modal-class="text-center">
      <template v-slot:modal-title>Consulta agendada com sucesso</template>
      <p>Em breve você receberá um email com os próximos passos.</p>
      <h6>Data da consulta:</h6>
      <p>{{ reservationStart | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservationEnd | date('H:mm') }}</p>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./calendar-paciente.ts"></script>
