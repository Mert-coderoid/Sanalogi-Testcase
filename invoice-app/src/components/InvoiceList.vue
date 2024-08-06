<template>
  <div class="container">
    <h1>Invoices</h1>
    <ul>
      <li v-for="invoice in invoices" :key="invoice.id">
        <div>
          <strong>Id:</strong> {{ invoice.id }} <br />
          <strong>Date:</strong> {{ new Date(invoice.date).toLocaleDateString() }} <br />
          <strong>Total Amount:</strong> {{ invoice.totalAmount }} <br />
          <strong>Products:</strong>
          <ul>
            <li v-for="product in invoice.products" :key="product.id">
              <strong>Product Name:</strong> {{ product.name }} <br />
              <strong>Price:</strong> {{ product.price }}
            </li>
          </ul>
        </div>
        <div>
          <button @click="editInvoice(invoice.id)">Edit</button>
          <button @click="deleteInvoice(invoice.id)" class="remove">Delete</button>
        </div>
      </li>
    </ul>
    <button @click="showModal = true">New Invoice</button>
    <InvoiceModal v-if="showModal" @close="showModal = false" @refresh="fetchInvoices" />
    <InvoiceEditModal
      v-if="showEditModal"
      :id="editInvoiceId"
      @close="showEditModal = false"
      @refresh="fetchInvoices"
    />
  </div>
</template>

<script>
import { useInvoiceStore } from '@/stores/invoiceStore'
import InvoiceModal from '@/components/InvoiceModal.vue'
import InvoiceEditModal from '@/components/InvoiceEditModal.vue'

export default {
  components: {
    InvoiceModal,
    InvoiceEditModal
  },
  data() {
    return {
      showModal: false,
      showEditModal: false,
      editInvoiceId: null
    }
  },
  computed: {
    invoices() {
      const store = useInvoiceStore()
      return store.invoices
    }
  },
  created() {
    const store = useInvoiceStore()
    store.fetchInvoices()
  },
  methods: {
    fetchInvoices() {
      const store = useInvoiceStore()
      store.fetchInvoices()
    },
    deleteInvoice(id) {
      const store = useInvoiceStore()
      store.deleteInvoice(id)
    },
    editInvoice(id) {
      this.editInvoiceId = id
      this.showEditModal = true
    }
  }
}
</script>

<style>
.container {
  max-width: 800px;
  margin: 20px auto;
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}
ul {
  list-style: none;
  padding: 0;
}
li {
  background: white;
  margin: 10px 0;
  padding: 10px;
  border: 1px solid #ddd;
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
button {
  padding: 5px 10px;
  margin-left: 5px;
  border: none;
  border-radius: 3px;
  background-color: #4caf50; /* Buton rengi değiştirildi */
  color: white;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
button:hover {
  background-color: #45a049; /* Hover rengi değiştirildi */
}
button.remove {
  background-color: #f44336;
}
button.remove:hover {
  background-color: #e53935;
}
button.add-product {
  background-color: #2196f3;
}
button.add-product:hover {
  background-color: #1e88e5;
}
</style>
