import { defineStore } from 'pinia'
import apiClient from '../services/api'

export const useInvoiceStore = defineStore('invoice', {
  state: () => ({
    invoices: [],
    invoice: null,
  }),
  actions: {
    fetchInvoices() {
      apiClient.get('/Invoice')
        .then(response => {
          this.invoices = response.data
        })
        .catch(error => {
          console.error(error)
        })
    },
    fetchInvoice(id) {
      return apiClient.get(`/Invoice/${id}`)  // Burada `return` ekledik
        .then(response => {
          this.invoice = response.data
          return response
        })
        .catch(error => {
          console.error(error)
          throw error
        })
    },
    addInvoice(invoice) {
      return apiClient.post('/Invoice', invoice)
        .then(() => {
          this.fetchInvoices()
        })
        .catch(error => {
          console.error(error)
          throw error
        })
    },
    updateInvoice(id, invoice) {
      return apiClient.put(`/Invoice/${id}`, invoice)
        .then(() => {
          this.fetchInvoices()
        })
        .catch(error => {
          console.error(error)
          throw error
        })
    },
    deleteInvoice(id) {
      return apiClient.delete(`/Invoice/${id}`)
        .then(() => {
          this.fetchInvoices()
        })
        .catch(error => {
          console.error(error)
          throw error
        })
    }
  }
})
