import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Conheca from './views/conheca.vue';
import SouPaciente from './views/sou-paciente.vue';
import SouPsicologo from './views/sou-psicologo.vue';

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
      path: '/conheca-a-aus-ouvidos',
      name: 'conheca',
      component: Conheca,
    },
    {
      path: '/sou-paciente',
      name: 'sou-paciente',
      component: SouPaciente,
    },
    {
      path: '/sou-psicologo',
      name: 'sou-psicologo',
      component: SouPsicologo,
    },
    {
      path: '*',
      component: () => import('./views/not-found.vue')
    },
  ],
  scrollBehavior(to, from, savedPosition) {
    return savedPosition ? savedPosition : { x: 0, y: 0 };
  },
});
