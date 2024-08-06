// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import InvoiceList from '@/components/InvoiceList.vue';

const routes = [
  {
    path: '/',
    name: 'InvoiceList',
    component: InvoiceList
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
