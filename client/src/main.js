import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import signalR from "./js/signalr";

Vue.config.productionTip = false
Vue.use(signalR)
new Vue({
  vuetify,
  render: function (h) { return h(App) }
}).$mount('#app')
