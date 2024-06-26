import { createRouter as createRouter, createWebHistory } from 'vue-router'
import { useStore } from 'vuex'

// Import components
import HomeView from '../views/HomeView.vue';
import LoginView from '../views/LoginView.vue';
import LogoutView from '../views/LogoutView.vue';
import RegisterView from '../views/RegisterView.vue';
import NotFoundView from "../views/NotFoundView.vue";
import TermsAndConditionsView from "../views/TermsAndConditionsView.vue";
import ListUnitView from '../views/ListUnitView.vue';
import UnitDetailsView from '../views/UnitDetailsView.vue';
import UserBidView from '../views/UserBidView.vue';
import AdminDetailsView from '../views/AdminDetailsView.vue'

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */
const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: {
      requiresAuth: false,
      hideNavigation: true
    }
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/login/terms",
    name: "terms",
    component: TermsAndConditionsView,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/logout",
    name: "logout",
    component: LogoutView,
    meta: {
      requiresAuth: false
    }
  },
  {
    path: "/register",
    name: "register",
    component: RegisterView,
    meta: {
      requiresAuth: false
    }
  },

  {
    path: "/admin/units/:unitId",
    name: "adminDetails",
    component: AdminDetailsView,
    meta: {
      requiresAuth: true
    }
  },

  {
    path: "/units",
    name: "units",
    component: ListUnitView,
    meta: {
      requiresAuth: false
    }
  },

  {
    path: "/units/:unitId",
    name: "unitDetails",
    component: UnitDetailsView,
    meta: {
      requiresAuth: false
    }
  },

  {
    path: "/units/:unitId/edit",
    name: "adminDetailsView",
    component: AdminDetailsView,
    meta: {
      requiresAuth: true
    }
  },

  {
    path: "/bids",
    name: "bids",
    component: UserBidView,
    meta: {
      requiresAuth: true
    }
  },

  {
    path: '/:pathMatch(.*)*',
    name: "notFound",
    component: NotFoundView,
  },

];

// Create the router
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
  scrollBehavior() {
    return {top: 0, left: 0}
  }
});

router.beforeEach((to) => {

  // Get the Vuex store
  const store = useStore();

  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    console.log("Not logged in. Redirected to login view.")
    return {name: "login"};
  }
  // Otherwise, do nothing and they'll go to their next destination
});

export default router;
