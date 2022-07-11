import Vue from 'vue';
import Router from 'vue-router';
import analytics from './utils/analytics';
import Home from './views/Home.vue';
import Conheca from './views/conheca.vue';
import SouPaciente from './views/sou-paciente.vue';
import SouPsicologo from './views/sou-psicologo.vue';
import EmpresasPendenteAprovacao from './views/empresas-pendente-aprovacao.vue';
import userService from './services/user.service';

Vue.use(Router);

const router =  new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: home,
      meta: { title: 'Home' },
    },
    {
      path: '/conheca-o-projeto',
      name: 'conheca',
      component: Conheca,
      meta: { title: 'Conheça o projeto' },
    },
    {
      path: '/eu-quero-conversar',
      name: 'sou-paciente',
      component: SouPaciente,
      meta: { title: 'Eu quero conversar' },
    },
    {
      path: '/eu-quero-escutar',
      name: 'sou-psicologo',
      component: SouPsicologo,
      meta: { title: 'Eu quero escutar' },
    },
    {
      path: '/admin/empresas-pendentes-aprovacao',
      name: 'empresas-pendente-aprovacao',
      component: EmpresasPendenteAprovacao,
      meta: { title: 'Empresas - pendente aprovação', requireAdmin: true },
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

router.beforeEach(async ({ meta, path }, from, next) => {
  const checkAdmin =  async () => {
    try {
      return await userService.isAdmin();
    } catch {
      await userService.signIn();
      return await userService.isAdmin();
    }
  };

  if (meta.requireAdmin) {
    const isAdmin = await checkAdmin();
    if (isAdmin) {
      return next();
    }

    return next('/');
  } else {
    return next();
  }
});

router.afterEach(({ meta, path }) => {
  const suffix = 'Aus Ouvidos';
  document.title = (meta && meta.title) ? `${meta.title} | ${suffix}` : suffix;
  analytics.pageView(path);
});

export default router;
