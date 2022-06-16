<template>
  <div class="container-xl">
    <div v-if="isLoggedIn === true">
      <h1>Eu quero escutar</h1>
      <p>Adicione um horário em sua agenda para o atendimento.</p>
      <calendar-psicologo @unauthorizedRequest="onUnauthorized"></calendar-psicologo>
    </div>
    <div v-if="isLoggedIn === false">
      <div class="row">
        <div class="col-sm-8">
          <h1>A escuta reinventando laços e recriando espaços</h1>
          <p>Aus Ouvidos é a reinvenção da escuta.</p>
          <p>A sensibilidade e solidariedade que toca o nosso grupo de profissionais psicólogos e psicanalistas nos apoia na realização de modo voluntário das escutas pontuais.</p>
          <p>As escutas pontuais seguem o rigor ético do sigilo ao qual nos conduzimos a partir do código de ética da psicologia, pautando com todo rigor de um trabalho responsável, respeitoso e qualificado para que o encontro humano se realize.</p>
          <p>A modalidade de uma nova escuta que nomeamos de escutas pontuais, apresenta como objetivo: cuidar de ouvir sem julgamentos, dialogar, recriar espaços e possibilitar uma nova perspectiva.</p>
          <p>Escutar é partilhar saberes, é tecer experiências e reflexões sobre a importância dos movimentos singulares e coletivos que reverberam em cada um de nós.</p>
          <p>O protagonismo da escuta é nosso compromisso, com a arte de escutar compomos espaços, criando e reinventando a fala. Um exercício que faz uma nova cena acontecer. Nessa poesia do encontro a escuta é capaz de produzir efeitos transformadores de modo singular e coletivo.</p>
          <a
            class="btn btn-primary"
            href="https://forms.office.com/Pages/ResponsePage.aspx?id=IORgVXFfDECLvuUvrnLrbM81jcOmACpFlly95MtX789UN1pRRzNZS0o5MThCMlNZNkg1TDNRTjVIWC4u"
            target="_blank">
            Clique e torne-se um voluntário
            <svg class="ml-1 text-big" aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
              <path d="M23.5 23.5h-15v-15h4.791V6H6v20h20v-7.969h-2.5z"/><path d="M17.979 6l3.016 3.018-6.829 6.829 1.988 1.987 6.83-6.828L26 14.02V6z"/>
            </svg>
          </a>
        </div>
        <div class="col-sm-3 mt-5 mt-sm-0">
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
