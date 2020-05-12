<template>
  <div class="container-xl">
    <div v-if="isLoggedIn === true">
      <h1>Eu quero ajudar</h1>
      <p>Adicione um horário em sua agenda para o atendimento.</p>
      <calendar-psicologo @unauthorizedRequest="onUnauthorized"></calendar-psicologo>
    </div>
    <div v-if="isLoggedIn === false">
      <div class="row">
        <div class="col-sm-8">
          <h1>Projeto Cuidando de Quem Cuida</h1>
          <p>Escutar quem se expõe na linha de frente da COVID-19 é a proposta realizada por Aus Ouvidos. </p>
          <p>Através da sensibilidade e da solidariedade, reunimos um grupo de profissionais na área da psicologia e psicanálise que se disponibilizam a realizar escutas pontuais.</p>
          <p>Nesse momento nosso público envolve: os médicos, a equipe de enfermagem, e a equipe de limpeza e segurança.</p>
          <p>Serão realizadas escutas por meio de vídeo chamada online, cumprindo com a orientação e resolução do CFF11/2018 e CRP11-09/2020.</p>
          <p>Nós, Aus Ouvidos, agradecemos imensamente a cada um que se dispõe a colaborar e integrar aqui a proposta ‘Cuidando de quem Cuida’.</p>
          <p>Não estamos sozinhos e, pela escuta nos tornamos agentes transformadores e apenas coletivamente ecoaremos entre tantas vidas.</p>
          <a
            class="btn btn-primary"
            href="https://forms.office.com/Pages/ResponsePage.aspx?id=IORgVXFfDECLvuUvrnLrbM81jcOmACpFlly95MtX789UN1pRRzNZS0o5MThCMlNZNkg1TDNRTjVIWC4u"
            target="_blank">
            Participe deste projeto
            <svg class="ml-1 text-big" aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
              <path d="M23.5 23.5h-15v-15h4.791V6H6v20h20v-7.969h-2.5z"/><path d="M17.979 6l3.016 3.018-6.829 6.829 1.988 1.987 6.83-6.828L26 14.02V6z"/>
            </svg>
          </a>
        </div>
        <div class="col-sm-4 mt-5 mt-sm-0">
          <div class="card">
            <div class="card-body">
              <h2 class="card-title">Área restrita</h2>
              <button class="btn btn-primary" @click="signIn">Fazer login</button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <b-modal
      ref="session-expired-modal"
      no-close-on-esc
      no-close-on-backdrop
      ok-only
      ok-title="Fazer login"
      @ok="signIn">
      <template v-slot:modal-title>Sua sessão expirou</template>
      <p>Clique em "Fazer login" para renovar sua sessão.</p>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./sou-psicologo.ts"></script>
