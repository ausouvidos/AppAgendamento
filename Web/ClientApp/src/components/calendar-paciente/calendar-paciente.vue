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

    <b-modal
      ref="reservation-modal"
      :modal-class="{'loading':isLoading}"
      :no-close-on-esc="isLoading"
      :no-close-on-backdrop="isLoading"
      cancel-title="Cancelar"
      cancel-variant="outline-primary"
      ok-title="Agendar"
      @ok="handleOk">
      <template v-slot:modal-title>Agendar uma consulta</template>
      <p class="font-weight-bold">{{ reservation.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservation.end | date('H:mm') }}</p>
      <validation-observer ref="observer">
        <form @submit="reserveSpot">
          <div class="form-group">
            <label for="reservation-name">Nome<small aria-hidden="true">*</small></label>
            <validation-provider name="Nome" rules="required" v-slot="{ classes, errors }">
              <input
                type="text"
                id="reservation-name"
                v-model="reservation.name"
                :class="['form-control', classes]"
                :disabled="isLoading">
              <span class="invalid-feedback">{{ errors[0] }}</span>
            </validation-provider>
          </div>
          <div class="form-group">
            <label for="reservation-email">Email<small aria-hidden="true">*</small></label>
            <validation-provider name="Email" rules="required|email" v-slot="{ classes, errors }">
              <input
                type="email"
                id="reservation-email"
                v-model="reservation.email"
                :class="['form-control', classes]"
                :disabled="isLoading">
              <span class="invalid-feedback">{{ errors[0] }}</span>
            </validation-provider>
          </div>
          <div class="form-group">
            <label for="reservation-mobile">Telefone<small aria-hidden="true">*</small></label>
            <validation-provider name="Telefone" rules="required|phone" v-slot="{ classes, errors }">
              <the-mask
                type="tel"
                id="reservation-mobile"
                placeholder="(99) 99999-9999"
                v-model="reservation.mobile"
                :class="['form-control', classes]"
                :mask="['(##) ####-####', '(##) #####-####']"
                :disabled="isLoading" />
              <span class="invalid-feedback">{{ errors[0] }}</span>
            </validation-provider>
          </div>
          <div class="form-group">
            <label for="reservation-voucher">Voucher<small aria-hidden="true">*</small></label>
            <validation-provider name="Voucher" rules="required" v-slot="{ classes, errors }">
              <input
                type="text"
                id="reservation-voucher"
                v-model="reservation.voucher"
                :class="['form-control', classes]"
                :disabled="isLoading">
              <span class="invalid-feedback">{{ errors[0] }}</span>
            </validation-provider>
          </div>
          <div id="recaptcha-container"></div>
          <validation-provider rules="required" v-slot="{ errors }">
            <input ref="captchaInput" type="hidden" v-model="reservation.recaptchaResponse">
            <small class="text-danger" v-if="errors.length">É necessário realizar a verificação de segurança</small>
          </validation-provider>
          <div v-if="hasFailed" class="text-danger mt-3">{{ errorMessage || 'Ocorreu um erro ao agendar a consulta, por favor tente novamente.'}}</div>
        </form>
      </validation-observer>
    </b-modal>

    <b-modal ref="confirmation-modal" hide-footer no-close-on-esc no-close-on-backdrop modal-class="text-center">
      <template v-slot:modal-title>Consulta agendada com sucesso</template>
      <p>Em breve você receberá um email com os próximos passos.</p>
      <h6 class="font-weight-bold mt-5">Data da consulta:</h6>
      <p>{{ reservation.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservation.end | date('H:mm') }}</p>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./calendar-paciente.ts"></script>
