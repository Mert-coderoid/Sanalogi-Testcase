<template>
  <div class="modal">
    <div class="modal-content">
      <h2>New Invoice</h2>
      <form @submit.prevent="submitInvoice">
        <div>
          <label>Date</label>
          <input type="date" v-model="invoice.date" required />
        </div>
        <div v-for="(product, index) in invoice.products" :key="index">
          <label>Product Name</label>
          <input v-model="product.name" required />
          <label>Price</label>
          <input type="number" v-model="product.price" required />
          <button type="button" @click="removeProduct(index)" class="remove">Remove</button>
        </div>
        <button type="button" @click="addProduct" class="add-product">Add Product</button>
        <div class="total">Total Amount: {{ totalAmount }}</div>
        <div class="actions">
          <button type="submit">Save</button>
          <button type="button" @click="$emit('close')">Cancel</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { useInvoiceStore } from '@/stores/invoiceStore'
import { useToast } from 'vue-toastification'

export default {
  data() {
    return {
      invoice: {
        totalAmount: 0,
        date: '',
        products: [
          {
            name: '',
            price: 0
          }
        ]
      }
    }
  },
  setup() {
    const toast = useToast()
    return { toast }
  },
  computed: {
    totalAmount() {
      return this.invoice.products.reduce((total, product) => total + Number(product.price), 0)
    }
  },
  methods: {
    addProduct() {
      this.invoice.products.push({ name: '', price: 0 })
    },
    removeProduct(index) {
      this.invoice.products.splice(index, 1)
    },
    formatDate(date) {
      const d = new Date(date)
      const month = '' + (d.getMonth() + 1)
      const day = '' + d.getDate()
      const year = d.getFullYear()

      return [year, month.padStart(2, '0'), day.padStart(2, '0')].join('-')
    },
    handleErrors(errors) {
      errors.forEach((error) => {
        this.toast.error(error.errorMessage)
      })
    },
    submitInvoice() {
      this.invoice.totalAmount = this.totalAmount
      this.invoice.date = this.formatDate(this.invoice.date)
      const store = useInvoiceStore()
      store
        .addInvoice(this.invoice)
        .then(() => {
          this.toast.success('Invoice created successfully')
          this.$emit('close')
          this.$emit('refresh')
        })
        .catch((error) => {
          console.error(error)
          if (error.response && error.response.data) {
            this.handleErrors(error.response.data)
          } else {
            this.toast.error('Error creating invoice')
          }
        })
    }
  }
}
</script>

<style scoped>
.total {
  font-weight: bold;
  margin-top: 15px;
}
</style>
