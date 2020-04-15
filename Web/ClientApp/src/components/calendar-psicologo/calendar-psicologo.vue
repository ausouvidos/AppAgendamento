<template>
  <div>
    <full-calendar
      ref="fullCalendar"
      class="custom-calendar"
      defaultView="timeGridWeek"
      slotDuration="01:00"
      height="auto"
      @dateClick="handleDateClick"
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
      :events="availabilities"></full-calendar>

    <b-modal ref="my-modal">
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
      <template v-slot:modal-footer>
        <button type="button" class="btn btn-secondary" @click="closeAvailabilityModal">Cancelar</button>
        <button type="button" class="btn btn-primary" @click="addAvailability">Adicionar</button>
      </template>
    </b-modal>
  </div>
</template>

<style lang="scss">
.custom-calendar {
  .fc-event {
    font-weight: bold;
    background-color: #007bff;
    border-color: #007bff;
    cursor: pointer;
    color: #fff;

    &:hover {
      background-color: #0069d9;
      color: #fff;
    }
  }
}
</style>

<script lang="ts" src="./calendar-psicologo.ts"></script>
