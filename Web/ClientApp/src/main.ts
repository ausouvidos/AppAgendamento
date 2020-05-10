import 'core-js/stable';
import 'regenerator-runtime/runtime';
import Vue from 'vue';
import App from './App.vue';
import router from './router';
import './plugins';
import './filters';
import './registerServiceWorker';

import lang from 'element-ui/lib/locale/lang/pt-br';
import locale from 'element-ui/lib/locale';

locale.use(lang);


Vue.config.productionTip = false;

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
