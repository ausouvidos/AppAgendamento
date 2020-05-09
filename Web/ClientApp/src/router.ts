import Vue from 'vue';
import Router from 'vue-router';
import analytics from './utils/analytics';
import Home from './views/Home.vue';
import Conheca from './views/conheca.vue';
import SouPaciente from './views/sou-paciente.vue';
import SouPsicologo from './views/sou-psicologo.vue';

Vue.use(Router);

const router =  new Router({
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
      meta: { title: 'Conheça a Aus Ouvidos' },
    },
    {
      path: '/sou-paciente',
      name: 'sou-paciente',
      component: SouPaciente,
      meta: { title: 'Sou paciente' },
    },
    {
      path: '/sou-psicologo',
      name: 'sou-psicologo',
      component: SouPsicologo,
      meta: { title: 'Sou psicólogo' },
    },
    {
      path: '*',
      component: () => import('./views/not-found.vue'),
      meta: { title: 'Página não encontrada' },
    },
  ],
  scrollBehavior(to, from, savedPosition) {
    return savedPosition ? savedPosition : { x: 0, y: 0 };
  },
});

router.afterEach(({ meta, path }) => {
  const suffix = 'Aus Ouvidos';
  document.title = (meta && meta.title) ? `${meta.title} | ${suffix}` : suffix;
  analytics.pageView(path);
});

export default router;
