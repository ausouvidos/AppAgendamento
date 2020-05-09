<template>
  <div class="container-xl">
    <h1>Sou psicólogo</h1>
    <div v-if="isLoggedIn === true">
      <p>Adicione um horário em sua agenda para o atendimento.</p>
      <calendar-psicologo @unauthorizedRequest="onUnauthorized"></calendar-psicologo>
    </div>
    <div v-if="isLoggedIn === false">
      <p>É necessário estar logado para acessar esta página.</p>
      <button class="btn btn-primary" @click="signIn">Fazer login</button>
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
