"use client"
import Image from 'next/image'
import './page.css'
import { api } from "../lib/api"

async function cadastrar(userName: string, email: string, passwd: string) {
  try {
    const userData = {
      userName: userName,
      email: email,
      passwd: passwd
    };

    await api.post('/Login', userData);
    console.log('Usuário criado com sucesso');
  } catch (error) {
    console.error('Erro ao fazer a requisição:', error);
  }
}

export default function Home() {
  return (
    <main>
      <form action="Cadastrar" onSubmit={() => console.log('Cadastrou')}>
        <div className="Form">
          <div>
            <label htmlFor="name">Nome</label>
            <input type="text" id="userName" name="name" />
          </div>
          <div>
            <label htmlFor="email">E-mail</label>
            <input type="email" id="email" name="email" />
          </div>
          <div>
            <label htmlFor="passwd">Senha</label>
            <input type="password" id="passwd" name="password" />
          </div>
          <button type="submit">CADASTRAR</button>
        </div>
      </form>
    </main>
  )
}
