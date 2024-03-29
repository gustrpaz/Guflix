"use client"
import { api } from "@/lib/api";
import { useEffect, useState } from "react";
import { useRouter } from 'next/navigation'
import Trash from "../../assets/Trash.png"
import Image from "next/image";
import './filme.css'
interface Filme {
  idFilme: number,
  nomeFilme: string
  genero: string,
}
export default function Filme() {
  const [filmes, setFilmes] = useState<Filme[]>([]);
  const [nomeFilme, setNomeFilme] = useState('');
  const [novoNomeFilme, setNovoNomeFilme] = useState('');
  const [nomeGenero, setNomeGenero] = useState('');
  const [novoNomeGenero, setNovoNomeGenero] = useState('');
  const [filmeSelecionado, setFilmeSelecionado] = useState<number | undefined>(undefined);
 
  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem('token');
      return !!token;
    };
    if (!checkAuth()) {
      router.replace('/login');
      console.log('Não está logado', localStorage.getItem('token'))
    }
  });

  async function ListarFilmes() {
    try {
      const Resposta = await api.get('/Filmes');
      const ListaFilmes: Filme[] = Resposta.data;
      setFilmes(ListaFilmes);
    } catch (erro) {
      console.error('Erro ao realizar requisição:', erro);
    }
  }
  async function DeletarFilme(idFilme: number) {
    try {
      await api.delete(`/Filmes/${idFilme}`);
      ListarFilmes();
    } catch (erro) {
      console.error('Erro ao fazer requisição:', erro);
    }
  }
  async function CadastrarFilmes(nomeFilme: string, nomeGenero: string) {
    try {
      const FilmeData = {
        nomeFilme: nomeFilme,
        genero: nomeGenero,
      };
      await api.post('/Filmes', FilmeData);
      ListarFilmes();
      setNomeFilme('');
      setNomeGenero('');
    }
    catch (erro) {
      console.error('Erro ao fazer requisição', erro);
    }
  }
  async function AtualizarFilme(idFilme: number, nomeFilme: string, novoNomeGenero: string) {
    try {
      const FilmeAtualizado = {
        nomeFilme: nomeFilme,
        genero: novoNomeGenero,
      };
      await api.put(`/Filmes/${idFilme}`, FilmeAtualizado);
      ListarFilmes();
      setNovoNomeFilme('');
      setNovoNomeGenero('');
    }
    catch (erro) {
      console.error('Erro ao fazer requisição', erro)
    }
  }
  useEffect(() => {
    ListarFilmes();
  }, []);
  const router = useRouter();
  return (
      <div className="content-film">
        <form>
          <input
            type="text"
            placeholder="Filme"
            value={nomeFilme}
            onChange={(f) => setNomeFilme(f.target.value)}
          />
          <input
            type="text"
            placeholder="Gênero"
            value={nomeGenero}
            onChange={(f) => setNomeGenero(f.target.value)}
          />
          <button className="btn" onClick={(event) => {
            event.preventDefault()
            CadastrarFilmes(nomeFilme, nomeGenero);}}>CADASTRAR</button>
        </form>
        <table>
          <thead>
            <tr>
              <th colSpan={4}>FILMES</th>
            </tr>
            <tr>
              <th>NOME</th>
              <th>GÊNERO</th>
              <th>EXCLUIR</th>
            </tr>
          </thead>
          <tbody>
            {filmes.map((filme) => (
              <tr key={filme.idFilme}>
                <td>{filme.nomeFilme}</td>
                <td>{filme.genero}</td>
                <td>
                  <button className='trash' onClick={() => DeletarFilme(filme.idFilme)}>
                    <Image src={Trash} width={21} height={21} alt='Apagar' />
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <form>
          <div>
            <select
              name="filme"
              id="filme"
              value={filmeSelecionado !== undefined ? String(filmeSelecionado) : ''}
              onChange={(e) => setFilmeSelecionado(e.target.value !== '' ? Number(e.target.value) : undefined)}
            >
              <option value="">Selecionar filme</option>
              {filmes.map((f) => (
                <option key={f.idFilme} value={f.idFilme}>
                  {f.nomeFilme}
                </option>
              ))}
            </select>
          </div>
          <div>
            <input
              type="text"
              id="filmeInput"
              placeholder="Novo filme"
              value={novoNomeFilme}
              onChange={(e) => setNovoNomeFilme(e.target.value)}></input>
          </div>
          <div>
            <input
              type="text"
              id="generoInput"
              placeholder="Novo Gênero"
              value={novoNomeGenero}
              onChange={(e) => setNovoNomeGenero(e.target.value)}></input>
          </div>
          <div>
            <button className="btn"
              onClick={(event) => {
                event.preventDefault();
                filmeSelecionado !== undefined && AtualizarFilme(filmeSelecionado, novoNomeFilme, novoNomeGenero)
              }}>ATUALIZAR</button>
          </div>
        </form>
      </div >
  )
}