import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Sobre from './views/sobre.vue';
import AgendarConsulta from './views/agendar-consulta.vue';
import MinhaAgenda from './views/minha-agenda.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/sobre',
      name: 'sobre',
      component: Sobre,
    },
    {
      path: '/agendar-consulta',
      name: 'agendar-consulta',
      component: AgendarConsulta,
    },
    {
      path: '/minha-agenda',
      name: 'minha-agenda',
      component: MinhaAgenda,
    },
  ],
  scrollBehavior(to, from, savedPosition) {
    return savedPosition ? savedPosition : { x: 0, y: 0 };
  },
});
