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
      :events="events"
      @eventClick="eventClick"></full-calendar>

    <b-modal
      ref="availability-modal"
      :modal-class="{'loading':isLoading}"
      :no-close-on-esc="isLoading"
      :no-close-on-backdrop="isLoading"
      cancel-title="Cancelar"
      cancel-variant="outline-primary"
      ok-title="Adicionar"
      @ok="addAvailability">
      <template v-slot:modal-title>Adicionar horário</template>
      <div class="form-row align-items-end">
        <div class="form-group col-9">
          <input type="date" class="form-control" v-model="availabilityDate" :disabled="isLoading">
        </div>
        <div class="form-group col-3">
          <select class="form-control" v-model="availabilityTimeStart" :disabled="isLoading">
            <option value=""></option>
            <option v-for="(time, index) in availableTimes" :key="index">{{ time }}</option>
          </select>
        </div>
        <!-- <div class="form-group col-3">
          <select v-model="availabilityTimeEnd" class="form-control">
            <option value=""></option>
            <option v-for="(time, index) in availableTimes" :key="index">{{ time }}</option>
          </select>
        </div> -->
        <div v-if="hasFailed" class="text-danger mt-3">{{ errorMessage || 'Ocorreu um erro ao adicionar o horário, por favor tente novamente.'}}</div>
      </div>
    </b-modal>

    <b-modal
      ref="future-event-modal"
      :modal-class="{'loading':isLoading}"
      :no-close-on-esc="isLoading"
      :no-close-on-backdrop="isLoading"
      cancel-title="Fechar"
      cancel-variant="outline-primary"
      ok-title="Remover horário"
      ok-variant="danger"
      @ok="removeAvailability">
      <template v-slot:modal-title>Horário disponível</template>
      <div v-if="selectedEvent">
        <p class="font-weight-bold">{{ selectedEvent.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ selectedEvent.end | date('H:mm') }}</p>
        <p>Este horário está disponível para agendamento.</p>
        <div v-if="hasFailed" class="text-danger mt-3">{{ errorMessage || 'Ocorreu um erro ao remover o horário, por favor tente novamente.'}}</div>
      </div>
    </b-modal>

    <b-modal
      ref="past-event-modal"
      :modal-class="{'loading':isLoading}"
      :no-close-on-esc="isLoading"
      :no-close-on-backdrop="isLoading"
      cancel-title="Fechar"
      cancel-variant="outline-primary"
      ok-title="Salvar"
      @ok="completeAvailability">
      <template v-slot:modal-title>Detalhes da consulta</template>
      <div v-if="selectedEvent">
        <p class="font-weight-bold">{{ selectedEvent.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ selectedEvent.end | date('H:mm') }}</p>
        <p>
          Paciente:<br>
          {{ selectedEvent.customerName }} | {{ selectedEvent.customerEmail }}
        </p>
        <div class="form-group">
          <label>Observações</label>
          <textarea class="form-control" cols="50" rows="5" v-model="selectedEvent.observations"></textarea>
        </div>
        <div class="form-group">
          <label>Notas</label>
          <textarea class="form-control" cols="50" rows="5" v-model="selectedEvent.notes"></textarea>
        </div>
        <div v-if="hasFailed" class="text-danger mt-3">{{ errorMessage || 'Ocorreu um erro ao salvar os detalhes da consulta, por favor tente novamente.'}}</div>
      </div>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./calendar-psicologo.ts"></script>
