'use client'
import './page.css';
import { api } from '../lib/api';
import Link from 'next/link';
import { useEffect } from 'react';
import { useRouter } from 'next/navigation'

interface UserData {
  userName: string;
  email: string;
  passwd: string;
}

async function cadastrar(event: React.FormEvent<HTMLFormElement>) {
  event.preventDefault();
  const userName = (event.target as any).userName.value;
  const email = (event.target as any).email.value;
  const passwd = (event.target as any).password.value;

  try {
    const userData: UserData = {
      userName,
      email,
      passwd,
    };
    await api.post('/usuarios', userData);
    console.log('Usuário criado com sucesso');
  } catch (error) {
    console.error('Erro ao fazer a requisição:', error);
  }
}

export default function Home() {
  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem('token');
      return !token;
    };

    if (!checkAuth()) {
      router.push('/filme');
    }
  },);

  const router = useRouter();
  return (
    <main>
      <form onSubmit={cadastrar}>
        <div className="Form">
          <div>
            <label htmlFor="userName">Nome</label>
            <input type="text" id="userName" name="userName" />
          </div>
          <div>
            <label htmlFor="email">E-mail</label>
            <input type="email" id="email" name="email" />
          </div>
          <div>
            <label htmlFor="password">Senha</label>
            <input type="password" id="password" name="password" />
          </div>
          <div>
            <button type="submit">CADASTRAR</button>
          </div>
        </div>
      </form>
      <Link href={'/login'}>Já tenho conta</Link>
    </main>
  );
}
