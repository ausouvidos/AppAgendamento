<template>
    <div>
        <el-steps :active="currentStep" finish-status="success" simple style="margin-top: 20px; margin-bottom: 20px;">
            <el-step title="Suas informações"></el-step>
            <el-step title="Horários disponíveis"></el-step>
            <el-step title="Confirmação"></el-step>
        </el-steps>

        <div v-if="currentStep === 0">
            <p>Primeiro voc&ecirc; precisa informar seus dados para contato e o código de autorização recebido.</p>
            <validation-observer ref="observer">
                <form>
                    <div class="form-group">
                        <label for="reservation-name">Nome<small aria-hidden="true">*</small></label>
                        <validation-provider name="Nome" rules="required" v-slot="{ classes, errors }">
                            <input type="text"
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
                            <input type="email"
                                   id="reservation-email"
                                   v-model="reservation.email"
                                   :class="['form-control', classes]"
                                   :disabled="isLoading">
                            <span class="invalid-feedback">{{ errors[0] }}</span>
                        </validation-provider>
                    </div>
                    <div class="form-group">
                        <label for="reservation-mobile">Telefone<small aria-hidden="true">*</small></label>
                        <validation-provider name="Telefone" rules="required" v-slot="{ classes, errors }">
                            <vue-tel-input v-model="reservation.mobile" :disabled="isLoading" :class="['form-control', classes]" v-bind="phoneMaskConfig"></vue-tel-input>
                            <span class="invalid-feedback">{{ errors[0] }}</span>
                        </validation-provider>
                    </div>
                    <div class="form-group">
                        <label for="reservation-voucher" class="d-flex align-items-center">
                            <span>Autorização</span>
                            <small aria-hidden="true">*</small>
                            <a href="javascript:void(0)"
                               class="badge badge-pill badge-primary ml-1"
                               v-b-tooltip="'Código de acesso disponibilizado pela instituição na qual você trabalha'"
                               aria-label="O que é a autorização?">?</a>
                            <button @click="showCompanyModal" type="button" class="btn btn-link btn-sm ml-auto pr-0 pb-0">Não tenho uma autorização</button>
                        </label>
                        <validation-provider name="Voucher" rules="required" v-slot="{ classes, errors }">
                            <input type="text"
                                   id="reservation-voucher"
                                   v-model="reservation.voucher"
                                   :class="['form-control', classes]"
                                   :disabled="isLoading">
                            <span class="invalid-feedback">{{ errors[0] }}</span>
                        </validation-provider>
                    </div>
                    <div id="recaptcha-container"></div>
                    <validation-provider rules="required" v-slot="{ errors }">
                        <input ref="reservation-captcha" type="hidden" v-model="reservation.recaptchaResponse">
                        <small class="text-danger" v-if="errors.length">É necessário realizar a verificação de segurança</small>
                    </validation-provider>
                    <div v-if="hasFailed" class="text-danger mt-3">{{ errorMessage || 'Ocorreu um erro ao agendar a consulta, por favor tente novamente.'}}</div>
                </form>
            </validation-observer>
        </div>

        <div v-if="currentStep === 1">
            <p>Agora selecione um horário na data desejada para que o agendamento possa ser realizado.</p>

            <el-alert v-if="hasFailed"
                      title="Falha ao agendar um horário"
                      type="error"
                      :description="errorMessage || 'Ocorreu um erro ao agendar a consulta, por favor tente novamente.'"
                      show-icon>
            </el-alert>

          
            <div class="scheduler">
                <div class="week-header">
                    <button class="btn btn-week-nav" title="Semana anterior" :disabled="schedule.isFirstWeek" @click="changeWeek(-1)">
                        <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
                            <path d="M14.19 16.005l7.869 7.868-2.129 2.129-9.996-9.997L19.937 6.002l2.127 2.129z" />
                        </svg>
                    </button>
                    <h2 class="week-title">{{ schedule.displayTitle }}</h2>
                    <button class="btn btn-week-nav btn-week-nav-next" title="Próxima semana" @click="changeWeek(1)">
                        <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
                            <path d="M18.629 15.997l-7.083-7.081L13.462 7l8.997 8.997L13.457 25l-1.916-1.916z" />
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
                                <path fill-rule="evenodd" d="M14 0H2a2 2 0 00-2 2v12a2 2 0 002 2h12a2 2 0 002-2V2a2 2 0 00-2-2zM1 3.857C1 3.384 1.448 3 2 3h12c.552 0 1 .384 1 .857v10.286c0 .473-.448.857-1 .857H2c-.552 0-1-.384-1-.857V3.857z" clip-rule="evenodd" />
                                <path fill-rule="evenodd" d="M6.5 7a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm-9 3a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm-9 3a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2zm3 0a1 1 0 100-2 1 1 0 000 2z" clip-rule="evenodd" />
                            </svg>
                            {{ day.date | date('dddd, D [de] MMMM') }}
                        </h3>
                        <button :class="{ 'btn btn-outline-primary': spotIndex !== indexSpot, 'btn btn-primary': spotIndex === indexSpot }"
                                @click="showReservationModal(spot, indexSpot)"
                                v-for="(spot, indexSpot) in day.availableSpots"
                                :key="`spot-${indexSpot}`">
                            {{ spot.start | date('HH:mm') }}
                        </button>
                    </div>
                </div>
                <div v-else class="scheduler-disclaimer">
                    Nenhum horário disponível na semana selecionada.
                </div>
            </div>

        </div>
        <div v-if="currentStep === 2">
            <p><b>Consulta agendada com sucesso</b></p>
            <p>Em breve você receberá um email com os próximos passos.</p>
            <h6 class="font-weight-bold mt-5">Data da consulta:</h6>
            <p>{{ reservation.start | date('dddd | DD/MM/YYYY | [das] H:mm') }} às {{ reservation.end | date('H:mm') }}</p>
        </div>
        <div class="btn-step-nav" v-if="currentStep < 2">
            <el-button plain @click="validateAndNext">{{stepButtonText}}</el-button>
        </div>


            <b-modal ref="company-modal"
                     :modal-class="{'loading':isLoading}"
                     :no-close-on-esc="isLoading"
                     :no-close-on-backdrop="isLoading"
                     cancel-title="Cancelar"
                     cancel-variant="outline-primary"
                     ok-title="Enviar"
                     @ok="handleOkCompany">
                <template v-slot:modal-title>
                    Cadastre sua instituição
                </template>
                <p>Se você acredita que a instituição na qual você trabalha não faz parte do nosso projeto, você pode sugerí-la para agilizar este processo.</p>
                <validation-observer ref="observerCompany">
                    <form @submit="addCompany">
                        <div class="form-group">
                            <label for="company-name">Nome da instituição<small aria-hidden="true">*</small></label>
                            <validation-provider name="Nome da instituição" rules="required" v-slot="{ classes, errors }">
                                <input type="text"
                                       id="company-name"
                                       v-model="newCompany.name"
                                       :class="['form-control', classes]"
                                       :disabled="isLoading">
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </validation-provider>
                        </div>

                        <div class="form-row">

                            <div class="form-group col-sm">
                                <label for="company-city">Cidade<small aria-hidden="true">*</small></label>
                                <validation-provider name="Cidade" rules="required" v-slot="{ classes, errors }">
                                    <input type="text"
                                           id="company-city"
                                           v-model="newCompany.city"
                                           :class="['form-control', classes]"
                                           :disabled="isLoading">
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </validation-provider>
                            </div>
                            <div class="form-group col-sm">
                                <label for="company-state">Estado<small aria-hidden="true">*</small></label>
                                <validation-provider name="Estado" rules="required" v-slot="{ classes, errors }">
                                    <input type="text"
                                           id="company-state"
                                           v-model="newCompany.state"
                                           :class="['form-control', classes]"
                                           :disabled="isLoading">
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </validation-provider>
                            </div>

                        </div>
                        <div class="form-row">
                            <div class="form-group col-sm">
                                <label for="company-phone">Telefone<small aria-hidden="true">*</small></label>
                                <validation-provider name="Telefone" rules="required" v-slot="{ classes, errors }">
                                    <vue-tel-input v-model="newCompany.phone" :disabled="isLoading" :class="['form-control', classes]" v-bind="phoneMaskConfig"></vue-tel-input>
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </validation-provider>
                            </div>
                            <div class="form-group col-sm">
                                <label for="company-contactPerson">Nome para contato<small aria-hidden="true">*</small></label>
                                <validation-provider name="Nome para contato" rules="required" v-slot="{ classes, errors }">
                                    <input type="text"
                                           id="company-contactPerson"
                                           v-model="newCompany.contactPerson"
                                           :class="['form-control', classes]"
                                           :disabled="isLoading">
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </validation-provider>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="company-contactPersonEmail">E-mail para contato<small aria-hidden="true">*</small></label>
                            <validation-provider name="Email" rules="required|email" v-slot="{ classes, errors }">
                                <input type="email"
                                       id="company-contactPersonEmail"
                                       v-model="newCompany.contactPersonEmail"
                                       :class="['form-control', classes]"
                                       :disabled="isLoading">
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </validation-provider>
                        </div>
                        <div id="recaptcha-container-company"></div>
                        <validation-provider rules="required" v-slot="{ errors }">
                            <input ref="company-captcha" type="hidden" v-model="newCompany.recaptchaResponse">
                            <small class="text-danger" v-if="errors.length">É necessário realizar a verificação de segurança</small>
                        </validation-provider>
                        <div v-if="hasFailed" class="text-danger mt-3">{{ errorMessage || 'Ocorreu um erro ao agendar a consulta, por favor tente novamente.'}}</div>
                    </form>
                </validation-observer>
            </b-modal>

            <b-modal ref="company-confirmation-modal" hide-footer no-close-on-esc no-close-on-backdrop modal-class="text-center">
                <template v-slot:modal-title>
                    Sugestão enviada com sucesso
                </template>
                <p>Obrigado! Em breve entraremos em contato com a instituição sugerida.</p>
            </b-modal>
</div>
</template>

<script lang="ts" src="./calendar-paciente.ts"></script>
<style scoped>
    .btn-step-nav {
        display: flex;
        flex-direction: row;
        justify-content: flex-end;
    }
</style>