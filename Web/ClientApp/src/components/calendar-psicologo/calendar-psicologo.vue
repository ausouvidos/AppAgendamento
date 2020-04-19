<template>
  <div>
    <full-calendar
      ref="fullCalendar"
      class="calendar-custom"
      defaultView="timeGridWeek"
      slotDuration="01:00"
      height="auto"
      :customButtons="customButtons"
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
        right: 'addEvent'
      }"
      :locale="calendarLocale"
      :plugins="calendarPlugins"
      :events="events"></full-calendar>

    <b-modal ref="availability-modal" ok-title="Adicionar" cancel-title="Cancelar" cancel-variant="outline-primary" @ok="addAvailability">
      <template v-slot:modal-title>Adicionar horário</template>
      <div class="form-row align-items-end">
        <div class="form-group col-6">
          <input v-model="availabilityDate" type="date" class="form-control">
        </div>
        <div class="form-group col-3">
          <select v-model="availabilityTimeStart" class="form-control">
            <option value=""></option>
            <option v-for="(time, index) in availableTimes" :key="index">{{ time }}</option>
          </select>
        </div>
        <div class="form-group col-3">
          <select v-model="availabilityTimeEnd" class="form-control">
            <option value=""></option>
            <option v-for="(time, index) in availableTimes" :key="index">{{ time }}</option>
          </select>
        </div>
      </div>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./calendar-psicologo.ts"></script>
