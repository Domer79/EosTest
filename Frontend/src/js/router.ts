import VueRouter from 'vue-router';
import HomeComponent from './views/home';
import AccessDeniedComponent from './views/access-denied';
import MaxValueItemsView from './views/maxvalue-items';
import InitialFillComponent from './views/initial-fill';

const routes = [
    { path: '/', component: HomeComponent },
    { path: '/maxvalueitems', component: MaxValueItemsView },
    { path: '/maxvalueitems/cte', component: MaxValueItemsView, props: { cte: true } },
    { path: '/initial-fill', component: InitialFillComponent },
];

const router = new VueRouter({
    routes,
    mode: 'history'
});

router.onError((error: Error) => {
    if (error.message === "Access Denied")
        router.push("/access-denied");
});

export default router;