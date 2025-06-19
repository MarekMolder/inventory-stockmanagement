<script setup lang="ts">
import {computed, onMounted, ref} from "vue";
import {useRoute, useRouter} from "vue-router";
import type { ICurrentStockEnriched } from "@/domain/logic/ICurrentStockEnriched";
import { CurrentStockService } from "@/services/mvcServices/CurrentStockService";

const route = useRoute();
const router = useRouter();
const searchCode = ref("");
const searchName = ref("");
const storageRoomId = route.params.storageRoomId as string;

const service = new CurrentStockService();
const data = ref<ICurrentStockEnriched[]>([]);

onMounted(async () => {
  const result = await service.getByStorageRoomId(storageRoomId);
  data.value = result.data || [];
});

const goToProduct = (id: string) => {
  router.push(`/product/${id}`);
};

const editProduct = (id: string) => {
  router.push(`/editcurrentstock/${id}`);
};

const filteredCurrentStocks = computed(() => {
  return data.value.filter((currentStock) => {
    const matchesProductName =
      currentStock.productName.toLowerCase().includes(searchName.value.toLowerCase());

    const matchesProductCode =
      currentStock.productCode.toLowerCase().includes(searchCode.value.toLowerCase());
    return matchesProductName && matchesProductCode;
  });
});
</script>

<template>
  <main class="currentStock-wrapper">
    <h1 class="page-title">{{ $t('Current Stock') }}</h1>

    <div class="filter-bar">
      <!-- Vasak pool -->
      <div class="filter-controls">
        <input
          v-model="searchName"
          type="text"
          :placeholder="$t('Search by name')"
          class="search-box"
        />
        <input
          v-model="searchCode"
          type="text"
          :placeholder="$t('Search by code')"
          class="search-box"
        />
      </div>

      <router-link class="create-link" to="/createcurrentstock">+ {{ $t('Create New') }}</router-link>
    </div>


    <div class="table-scroll-container">
      <div class="currentStock-list">
        <div class="currentStock-card" v-for="item in filteredCurrentStocks" :key="item.id">
          <div class="currentStock-picture">
            <img alt="Product" />
          </div>
          <div class="currentStock-info">
            <div class="currentStock-header">
              <h3 class="product-title">{{ item.productName }}</h3>
              <div class="button-group">
                <button class="edit-link" @click="editProduct(item.id)">✎ {{ $t('Edit') }}</button>
                <button class="view-button" @click="goToProduct(item.productId)">{{ $t('View') }}</button>
              </div>
            </div>
            <p><strong>{{ $t('Product Code') }}:</strong> {{ item.productCode }}</p>
            <p><strong>{{ $t('Quantity') }}:</strong> {{ item.quantity }}</p>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<style scoped>
.currentStock-wrapper {
  padding: 2rem;
  font-family: Arial, sans-serif;
  background-color: #1a1a1a;
  color: white;
}

.page-title {
  text-align: center;
  font-size: 2rem;
  color: orange;
  margin-bottom: 1rem;
}

.table-scroll-container {
  max-height: 650px;
  overflow-y: auto;
  border: 1px solid #444;
  border-radius: 8px;
}

.currentStock-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(450px, 1fr));
  gap: 1.5rem;
}

.currentStock-card {
  display: flex;
  background-color: #2a2a2a;
  border: 2px solid orange;
  border-radius: 12px;
  padding: 1rem;
  gap: 1rem;
  box-shadow: 0 0 8px rgba(255, 165, 0, 0.2);
}

.currentStock-picture img {
  width: 100px;
  height: 100px;
  object-fit: cover;
  border-radius: 12px;
  background-color: #444;
}

.currentStock-info {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.currentStock-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 0.5rem;
}

.product-title {
  font-size: 1.2rem;
  color: orange;
  margin: 0;
}

.button-group {
  display: flex;
  gap: 0.5rem;
}

.edit-link,
.view-button {
  background-color: orange;
  padding: 0.4rem 0.9rem;
  color: white;
  border: none;
  border-radius: 6px;
  font-weight: bold;
  font-size: 0.9rem;
  cursor: pointer;
  transition: background 0.3s ease;
}

.edit-link:hover,
.view-button:hover {
  background-color: #ffaa33;
}

.search-box,
select {
  padding: 0.5rem;
  font-size: 1rem;
  border-radius: 6px;
  border: 2px solid orange;
  background-color: #2a2a2a;
  color: white;
}

.filter-bar {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  flex-wrap: wrap;
  margin-bottom: 1.5rem;
}

.create-link {
  white-space: nowrap;
}

.create-link {
  background-color: orange;
  padding: 0.5rem 1rem;
  color: white;
  text-decoration: none;
  border-radius: 6px;
  font-weight: bold;
  transition: background 0.3s ease;
}

.create-link:hover {
  background-color: #ffaa33;
}
</style>
