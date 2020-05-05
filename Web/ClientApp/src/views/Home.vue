<template>
  <div>
    <div class="banner banner-home">
      <img class="banner-image" src="@/assets/banner-home.jpg" alt="">
      <div class="banner-text">
        <h1>Aus Ouvidos é uma proposta de escuta<br class="d-none d-lg-block"> que se coloca aqui ao seu lado.</h1>
        <p>Falar das inseguranças, dos medos, das dores e de tudo que aflige é uma forma de<br class="d-none d-lg-block"> manter-se em protegido e proteger aos que de você dependem.</p>
      </div>
    </div>
    <div class="container-xl text-big">
      <div class="text-center">
        <h1>COVID-19: Como a minha vida é impactada?</h1>
        <p class="text-muted">Esta pandemia já é entendida como o maior problema sanitário de nossa época.</p>
      </div>
  
      <div class="home-box-1">
        <div class="row">
          <div class="col-md-5 mb-5 mb-md-0 text-center">
            <img class="img-fluid" src="@/assets/home-1.png" alt="">
          </div>
          <div class="col-md-7">
            <h4>Como está a minha vida, a vida de quem amo e a vida de quem cuido?</h4>
            <p>Quem sou eu hoje meio a uma rotina que desconheço e como consigo minimamente me manter organizado, com condições de me proteger dos riscos de me infectar e infectar outros. Essas são questões que causam angustias intensas nos profissionais que estão na linha de frente ao combate a COVID – 19.  Estamos aqui para oferecer uma escuta especializada.</p>
            <p class="font-weight-bold">Estamos aqui para ouvir você.</p>
            <router-link :to="{ name: 'agendar-consulta' }" class="btn btn-primary">Agende um horário</router-link>
          </div>
        </div>
      </div>
  
      <div class="row align-items-center home-box-2">
        <div class="col-md-7 mt-5 mb-5">
          <h1>Somos humanos, somos frágeis.</h1>
          <p class="font-weight-bold">O que está acontecendo?</p>
          <p>Nenhuma rotina de vida é mais a mesma, as rotinas de trabalho se tornam um desafio sem igual. Atenção e foco nos procedimentos causam ansiedade, stress e muitas vezes na sensação de impotência surge descontroles.</p>
          <p>Somos humanos e não podemos esquecer disso, cada vida que se perde é uma sensação de dor que cada um apresenta de um modo.</p>
          <p>Reconhecer a fragilidade é uma forma de enfrentamento. Estamos aqui para ouvir sobre suas angustias, falar é uma forma de reorganizar os pensamentos e reencontrar um ‘respiro’.</p>
          <router-link :to="{ name: 'sobre' }" class="btn btn-outline-primary">Conheça a Aus Ouvidos</router-link>
        </div>
        <div class="col-md-5 d-md-block d-none">
          <img class="img-fluid" src="@/assets/home-2.jpg" alt="">
        </div>
      </div>
    </div>

    <div class="home-box-3 text-big">
      <div class="container-xl">
        <h1><span>Tra</span>balhamos com <span>ta</span>ntos sofri<span>mentos</span></h1>
        <p>A emoção é uma condição humana, somos a soma do que sentimos. Alguns sintomas estão em nós nos lembrando disso:</p>
        <ul class="home-box-3-list">
          <li class="item-temperature">A temperatura aumenta</li>
          <li class="item-rope">Surge um nó na garganta</li>
          <li class="item-lungs">Dificuldade para respirar</li>
        </ul>
        <p>Estamos aqui para te ouvir. Falar sobre o que sente é uma forma de reorganização emocional.</p>
      </div>
    </div>

    <div class="container-xl text-big">
      <div class="row home-box-4">
        <div class="col-md text-center">
          <img class="img-fluid" src="@/assets/home-3.jpg" alt="">
        </div>
        <div class="col-md mt-4">
          <h1>Falar sobre essa angústia</h1>
          <p class="font-weight-bold">É uma forma de restaurar a vida em mim?</p>
          <p>SIM! Porque quando você fala, você constrói uma possibilidade de se reorganizar emocionalmente. Recria suas potencialidades para prosseguir nas atividades. Retoma o foco para suportar a tensão e não dispersar e se perder nas inúmeras regras de proteção.</p>
          <p>Falar das inseguranças, dos medos, das dores e de tudo que aflige é uma forma de manter-se em protegido e proteger aos que de você dependem.</p>
          <p>Aus Ouvidos é uma proposta de escuta que se coloca aqui ao seu lado.</p>
        </div>
      </div>
      <div class="text-center mt-3">
        <router-link :to="{ name: 'sobre' }" class="btn btn-outline-primary">Conheça a Aus Ouvidos</router-link>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Home extends Vue {}
</script>
