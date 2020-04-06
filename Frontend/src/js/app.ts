'use strict';

import Vue from 'vue';
import VueRouter from 'vue-router';
import Vuetify from 'vuetify';
import router from "./router";
import { format } from 'path';
import { Prop } from 'vue-property-decorator';
import LayoutController from './views/layout';
import moment from 'moment';

Vue.use(Vuetify);
moment.locale('ru');
moment.defaultFormat = 'DD.MM.YYYY HH:mm';

Vue.use(VueRouter);

const vuetify = new Vuetify();
new Vue({
    el: '#app',
    render: h => h(LayoutController),
    router,
    vuetify: vuetify
})