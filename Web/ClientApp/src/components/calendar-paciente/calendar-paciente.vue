<template>
  <div>
    <full-calendar
      ref="fullCalendar"
      class="custom-calendar"
      defaultView="timeGridWeek"
      slotDuration="01:00"
      height="auto"
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

<script lang="ts" src="./calendar-paciente.ts"></script>
