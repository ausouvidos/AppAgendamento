<template>
  <div>
    <div class="container-xl">
      <h2 class="members-title">{{title}}</h2>
      <div class="members-list">
        <div class="member-item" v-for="teamMember in items" :key="teamMember.id">
          <img class="member-photo" :src="teamMember.photoUrl" alt="">
          <h2 class="member-name">{{ teamMember.nomeCompleto }}</h2>
          <p>
            {{ teamMember.funcao }}
            <span class="d-block" v-if="teamMember.registro">{{ teamMember.registro }}</span>
          </p>
          <div class="member-social">
            <a v-if="teamMember.email" :href="`mailto:${teamMember.email}`" class="icon icon-mail" title="Email">
              <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 32 32" fill="currentColor">
                <path d="M5 24.225V7.776h22v16.447H5v.002zm3.011-1.815h15.978l-5.111-5.115L16 20.179l-2.877-2.883-5.112 5.114zm-1.216-1.275l5.077-5.09-5.077-5.065v10.155zm13.332-5.09l5.079 5.09V10.979l-5.079 5.066zm-4.126 1.588l8.022-8.027-16.045-.001 8.023 8.028z"/>
              </svg>
            </a>
            <a v-if="teamMember.linkedIn" :href="teamMember.linkedIn" class="icon icon-linkedin" title="LinkedIn" target="_blank">
              <svg aria-hidden="true" width="1em" height="1em" viewBox="0 0 24 24" fill="currentColor">
                <path d="M4.98 3.5c0 1.381-1.11 2.5-2.48 2.5s-2.48-1.119-2.48-2.5c0-1.38 1.11-2.5 2.48-2.5s2.48 1.12 2.48 2.5zm.02 4.5h-5v16h5v-16zm7.982 0h-4.968v16h4.969v-8.399c0-4.67 6.029-5.052 6.029 0v8.399h4.988v-10.131c0-7.88-8.922-7.593-11.018-3.714v-2.155z"/>
              </svg>
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" src="./equipe-item.ts"></script>

