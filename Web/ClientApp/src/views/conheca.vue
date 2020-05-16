<template>
  <div>
    <div class="banner">
      <img class="banner-image" src="@/assets/banner-sobre.jpg" alt="">
      <div class="banner-text bg-alpha"></div>
    </div>
    <div class="container-xl">
      <h1>Conheça o projeto Aus Ouvidos</h1>
      <div class="text-big">
        <p>A cada instante, somos tocados por algum sentimento de dor, medo, raiva e tantos outros sentimentos que apontam para nossa fragilidade. Com quem falamos? Quem nos ouve?</p>
        <p>A reorganização emocional começa pela escuta. Então, se questione: de que forma a escuta se estabelece para você e quais os laços são construídos a partir dela?</p>
        <p>Aus Ouvidos é uma proposta que busca refletir sobre essas questões.</p>
        <p>Buscamos escutar angústias, alegrias, dúvidas, histórias entre tantas outras possibilidades.</p>
        <p>Somos uma iniciativa multidisciplinar e construímos nossa escuta com recursos que envolvem a arte, a psicologia, a música, a arte cênica, a pedagogia e a engenharia.</p>
        <p>Acreditamos que escutar é oferecer um espaço para fala se recriar, compor, comunicar e se expressar. É movimentar algo e buscar sintonia e sincronia, promovendo  laços e permitindo novos passos.</p>
      </div>
    </div>
    <equipe />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Equipe from '@/components/equipe/equipe.vue';

@Component({
  components: {
    Equipe,
  },
})
export default class Conheca extends Vue {}
</script>
