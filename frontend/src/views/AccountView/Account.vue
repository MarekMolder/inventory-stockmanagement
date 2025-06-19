<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useUserDataStore } from '@/stores/userDataStore';
import { IdentityService } from '@/services/IdentityService';
import {RouterLink} from "vue-router";

const store = useUserDataStore();
const identityService = new IdentityService();

console.log("JWT:", store.jwt);
console.log("Email:", store.email);
console.log("FirstName:", store.firstName);
console.log("LastName:", store.lastName);
console.log("Username:", store.username);
console.log("Role:", store.role);

const firstName = ref('');
const lastName = ref('');
const userName = ref('');
const message = ref('');

onMounted(() => {
  firstName.value = store.firstName || '';
  lastName.value = store.lastName || '';
  userName.value = store.username || '';
});

const update = async () => {
  const result = await identityService.updateUserFields({
    email: store.email!,
    userName: userName.value,
    firstName: firstName.value,
    lastName: lastName.value,
  });

  message.value = result.errors ? `Viga: ${result.errors.join(', ')}` : 'Konto uuendatud!';
};

</script>

<template>
  <div class="page-wrapper">
    <div class="account-panel">
      <h2>{{ $t('My account') }}</h2>
      <div class="profile-picture">👤</div>

      <div class="important-info">
        <p><strong>{{ $t('Firsname') }}:</strong> {{ store.firstName }}</p>
        <p><strong>{{ $t('Lastname') }}:</strong> {{ store.lastName }}</p>
        <p><strong>{{ $t('Username') }}:</strong> {{ store.username }}</p>
        <p><strong>{{ $t('Email') }}:</strong> {{ store.email }}</p>
      </div>
    </div>

    <div class="edit-panel">
      <h2>{{ $t('Edit Profile') }}</h2>
      <form @submit.prevent="update">
        <label>{{ $t('Firsname') }}</label>
        <input v-model="firstName" type="text" placeholder="Enter your firstName..." />

        <label>{{ $t('Lastname') }}</label>
        <input v-model="lastName" type="text" placeholder="Enter your lastName..." />

        <label>{{ $t('Username') }}</label>
        <input v-model="userName" type="text" placeholder="Enter your username..." />

        <div class="buttons">
          <button type="submit" class="save-button">{{ $t('Save data') }}</button>
          <router-link to="/changepassword" class="change-password">
            {{ $t('Change password') }}
          </router-link>
        </div>

        <p class="message">{{ message }}</p>
      </form>
    </div>
  </div>
</template>

<style scoped>

.page-wrapper {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: flex-start;
  gap: 2rem;
  padding: 2rem;
  font-family: "Segoe UI", sans-serif;
  color: white;
  min-height: 80vh;
}

.account-panel,
.edit-panel {
  flex: 1 1 350px;
  max-width: 500px;
  background-color: #1a1a1a;
  border-radius: 16px;
  padding: 2rem;
  box-shadow: 0 0 10px rgba(255, 165, 0, 0.4);
  border: 3px solid orange;
  margin-top: 5rem;
  height: 500px;
}

.profile-picture {
  width: 100px;
  height: 100px;
  background: orange;
  border-radius: 50%;
  margin: 1rem auto;
  font-size: 3rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.important-info {
  background: #ffffff;
  color: #000000;
  padding: 1.5rem;
  border-radius: 12px;
  text-align: left;
  font-size: 1rem;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  margin-top: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.important-info p {
  margin: 0;
  padding: 0.25rem 0;
  border-bottom: 1px solid #ddd;
}

.important-info p:last-child {
  border-bottom: none;
}

.edit-panel {
  flex: 1 1 600px;
  max-width: 900px;
}

.edit-panel form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.edit-panel label {
  font-weight: bold;
  margin-bottom: 0.2rem;
}

.edit-panel input[type="text"] {
  padding: 0.6rem;
  border-radius: 8px;
  border: none;
  background-color: #f5f5f5;
  color: #000000;
  font-weight: bold;
}

.buttons {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 1.5rem;
  gap: 1rem;
}

.save-button {
  background: linear-gradient(to top left, rgb(40, 25, 5), orange);
  border: 2px solid orange;
  border-radius: 8px;
  padding: 0.6rem 1.4rem;
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.3s ease;
}

.save-button:hover {
  background: linear-gradient(to right, #e69500, #d4af37);
}

.change-password {
  color: orange;
  text-decoration: underline;
  font-weight: bold;
  cursor: pointer;
  background: none;
  border: none;
  padding: 0;
}

.message {
  margin-top: 1rem;
  color: lightgreen;
  font-weight: bold;
}

</style>
