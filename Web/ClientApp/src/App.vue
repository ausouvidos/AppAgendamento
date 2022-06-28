<template>
  <div id="app" class="wrapper">
    <div
      :style="{
        'background-image': `url(${require('./assets/background.jpg')})`,
        width: '100%',
        height: '100%',
        'z-index': 999999,
        'background-repeat': 'no-repeat',
        'background-position': 'center',
        'background-size': 'cover',
        'min-height': '100vh',
      }"
    >
      <header class="header">
        <div class="header-inner container-xl bg-transparent">
          <a class="logo" href="https://www.ausouvidos.com/">
            <img
              src="./assets/Logo_AUS_FINAL.png"
              style="max-width: 200px;max-height: 200px;margin-top: 15px;margin-bottom: 15px;"
              alt="Projeto Aus Ouvidos">
          </a>
        </div>
      </header>
      <section class="main-content">
        <router-view />
      </section>
      <footer class="footer">
        <div class="container-footer">
        
          <img src="./assets/ausouvidos-footer.png" />
        
          <ul class="footer-social-ul">
            <li style="display: flex;align-items: center;">
              <img class="footer-social-img" src="./assets/linkedin-logo.png" />
            </li>
            <li style="display: flex;align-items: center;">
              <img class="footer-social-img" src="./assets/facebook-logo.png" />
            </li>
            <li style="display: flex;align-items: center;">
              <img class="footer-social-img" src="./assets/instagram-logo.png" />
            </li>
          </ul>
          <div style="display: flex;align-items: center;">
            <p style="font-size:16px; line-height:1.3em;">
              <span style="letter-spacing:0em;"><span style="font-size:16px;">2022 - Instituto Aus Ouvidos. Direitos Reservados.</span></span>
            </p>
          </div>
        </div>
      </footer>
    </div>
  </div>
</template>

<style src="./styles/main.scss" lang="scss"></style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class App extends Vue {}
</script>
