<template>
  <div>
    <div class="scheduler">
      <div class="week-header">
        <button class="btn btn-week-nav" title="Semana anterior" :disabled="schedule.isFirstWeek" @click="changeWeek(-1)">
          <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
            <path d="M14.19 16.005l7.869 7.868-2.129 2.129-9.996-9.997L19.937 6.002l2.127 2.129z"/>
          </svg>
        </button>
        <h2 class="week-title">{{ schedule.displayTitle }}</h2>
        <button class="btn btn-week-nav btn-week-nav-next" title="Próxima semana" @click="changeWeek(1)">
          <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
            <path d="M18.629 15.997l-7.083-7.081L13.462 7l8.997 8.997L13.457 25l-1.916-1.916z"/>
          </svg>
        </button>
      </div>
      <div v-if="isScheduleLoading" class="scheduler-disclaimer">
        Carregando...
      </div>
      <div v-else-if="schedule.days.length">
        <div class="week-day" v-for="(day, indexDay) in schedule.days" :key="`day-${indexDay}`">
          <h3 class="week-day-title">
            <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 16 16" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
              <path fill-rule="evenodd" d="M14 0H2a2 2 0 00-2 2v12a2 2 0 002 2h12a2 2 0 002-2V2a2 2 0 00-2-2zM1 3.857C1 3.384 1.448 3 2 3h12c.552 0 1 .384 1 .857v10.286c0 .473-.448.857-1 .857H2c-.552 0-1-.384-1-.857V3.857z" clip-rule="evenodd"/>
              <path fill-rule="evenodd" d="M6.5 7a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm-9 3a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm-9 3a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2z" clip-rule="evenodd"/>
            </svg>
            {{ day.date | date('dddd, D [de] MMMM') }}
          </h3>
          <button class="btn btn-outline-primary" @click="showReservationModal(spot)" v-for="(spot, indexSpot) in day.availableSpots" :key="`spot-${indexSpot}`">
            {{ spot.start | date('HH:mm') }}
          </button>
        </div>
      </div>
      <div v-else class="scheduler-disclaimer">
        Nenhum horário disponível na semana selecionada.
      </div>
    </div>

    <b-modal ref="reservation-modal" ok-title="Agendar" cancel-title="Cancelar" cancel-variant="outline-primary" @ok="reserveSpot">
      <template v-slot:modal-title>Agendar uma consulta</template>
      <p class="font-weight-bold">{{ reservation.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservation.end | date('H:mm') }}</p>
      <div class="form-group">
        <label for="reservation-name">Nome</label>
        <input type="text" class="form-control" id="reservation-name" v-model="reservation.name">
      </div>
      <div class="form-group">
        <label for="reservation-email">Email</label>
        <input type="email" class="form-control" id="reservation-email" v-model="reservation.email">
      </div>
      <div class="form-group">
        <label for="reservation-mobile">Telefone</label>
        <input type="tel" class="form-control" id="reservation-mobile" v-model="reservation.mobile">
      </div>
    </b-modal>

    <b-modal ref="confirmation-modal" hide-footer modal-class="text-center">
      <template v-slot:modal-title>Consulta agendada com sucesso</template>
      <p>Em breve você receberá um email com os próximos passos.</p>
      <h6 class="font-weight-bold mt-5">Data da consulta:</h6>
      <p>{{ reservation.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservation.end | date('H:mm') }}</p>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./calendar-paciente.ts"></script>
