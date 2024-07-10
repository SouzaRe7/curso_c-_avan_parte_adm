using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VendasAdm
{
    public static class XmlUtil
    {
        /// <summary>
        /// Função que tem o caminho para salvar/editar/consultar o arquivo xml
        /// </summary>
        /// <param name="arquivo"> Nome do arquivo xml </param>
        /// <returns> Caminho do arquivo </returns>
        public static string PathFileDbXml(string arquivo)=> "C:\\Users\\Usuario\\Desktop\\curso_logica_c\\VendasAdm\\db_xml\\" + arquivo + ".xml";

        /// <summary>
        /// Função que verifica se existe o código/id
        /// </summary>
        /// <param name="cod"> Código/Id </param>
        /// <param name="nomeArquivo"> Nome do arquivo xml </param>
        /// <returns> bool </returns>
        public static bool VerificarCodigoXmlUtil(string cod, string nomeArquivo)
        {
            XmlDocument xml = new XmlDocument();
            if (File.Exists(XmlUtil.PathFileDbXml(nomeArquivo)))
            {
                xml.Load(XmlUtil.PathFileDbXml(nomeArquivo));

                XmlNode xmlCod = xml.SelectSingleNode("//Item[Codigo='" + cod + "']");

                if (xmlCod != null)
                {
                    MessageBox.Show("Código existente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Função que cria o arquivo xml
        /// </summary>
        /// <param name="cod"> Código/Id do nó filho </param>
        /// <param name="nomeTabela"> Nome do nó pai </param>
        /// <param name="nomeArquivo"> Nome do arquivo xml </param>
        /// <param name="novosValores"> Dicionario com o(s) nome(s) do(s) nó(s) filho(s) e seus valores (Dictionary<string, string>) </param>
        public static void CadastrarUtilXML(string nomeCampoId, string nomeTabela, string nomeArquivo, Dictionary<string, string> novosValores)
        {
            // Cria o objeto xml
            XmlDocument xml = new XmlDocument();

            int cod;

            // Verifica se o arquivo não existe
            if (!File.Exists(XmlUtil.PathFileDbXml(nomeArquivo)))
            {
                // Se o arquivo não existir, cria o elemento raiz
                XmlElement root = xml.CreateElement(nomeTabela);
                xml.AppendChild(root);
                cod = 1;
            }
            else
            {
                xml.Load(XmlUtil.PathFileDbXml(nomeArquivo));

                // Encontra todos os elementos "Item" e obtém o maior valor de nomeCampoId
                XmlNodeList itemList = xml.SelectNodes($"{nomeTabela}/Item/{nomeCampoId}");
                if (itemList.Count > 0)
                {
                    cod = itemList.Cast<XmlElement>()
                                  .Max(e => int.Parse(e.InnerText)) + 1;
                }
                else
                {
                    cod = 1; // Inicia com 1, pois não há itens existentes
                }
            }

            // Cria um novo elemento "Item" para a nova pessoa
            XmlElement infoItem = xml.CreateElement("Item");

            XmlElement infoCodigo = xml.CreateElement(nomeCampoId);
            infoCodigo.InnerText = cod.ToString();
            infoItem.AppendChild(infoCodigo);

            // Adiciona os valores do dicionário como elementos filhos de "Item"
            foreach (var kvp in novosValores)
            {
                XmlElement infoElement = xml.CreateElement(kvp.Key);
                infoElement.InnerText = kvp.Value;
                infoItem.AppendChild(infoElement);
            }

            // Adiciona o elemento "Item" ao nó "Pessoa"
            XmlNode xmlPessoa = xml.SelectSingleNode(nomeTabela);
            xmlPessoa.AppendChild(infoItem);

            // Salva as alterações no arquivo XML
            xml.Save(XmlUtil.PathFileDbXml(nomeArquivo));
        }

        /// <summary>
        /// Função que altera o(s) nó(s) do arquivo xml
        /// </summary>
        /// <param name="cod"> Código/Id do nó filho </param>
        /// <param name="nomeArquivo"> Nome do arquivo xml </param>
        /// <param name="novosValores"> Dicionario com o(s) nome(s) do(s) nó(s) filho(s) e seus valores (Dictionary<string, string>) </param>
        public static void AlterarUtilXML(string cod, string nomeCampoId, string nomeArquivo, Dictionary<string, string> novosValores)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(XmlUtil.PathFileDbXml(nomeArquivo));

            XmlNode xmlInfoAlt = xml.SelectSingleNode("//Item["+ nomeCampoId + "='" + cod + "']");

            if (xmlInfoAlt != null)
            {
                foreach (var kvp in novosValores)
                {
                    XmlNode xmlInfoNode = xmlInfoAlt.SelectSingleNode(kvp.Key);
                    if (xmlInfoNode != null)
                    {
                        xmlInfoNode.InnerText = kvp.Value;
                    }
                }

                xml.Save(XmlUtil.PathFileDbXml(nomeArquivo));
            }
        }

        /// <summary>
        /// Função que carrega o arquivo xml no DataGridView
        /// </summary>
        /// <param name="dataGridView"> Nome do DataGridView </param>
        /// <param name="pathFile"> Caminho do arquivo xml </param>
        public static void CarregarGrdUtilXML(DataGridView dataGridView, string pathFile, string nomeCampoId, string idEmpresaLogada)
        {
            dataGridView.DataSource = null;

            if (File.Exists(pathFile))
            {
                DataSet data = new DataSet();

                data.ReadXml(pathFile);

                if (data.Tables.Count > 0)
                {
                    DataTable dataTable = data.Tables[0];
                    string filterExpression = $"{nomeCampoId} = '{idEmpresaLogada}'";
                    DataView dataView = new DataView(dataTable)
                    {
                        RowFilter = filterExpression
                    };
                    dataGridView.DataSource = dataView;   
                }
            } 
            else
            {
                dataGridView.DataSource = new DataTable();
            }
        }

        public static void CarregarRemoverCabecalhoListaUtilXML(DataGridView dataGridView, List<string> lista, string pathFile)
        {
            if (File.Exists(pathFile))
            {
                foreach (var item in lista)
                {
                    dataGridView.Columns[item].Visible = false;
                }
            }
            else
            {
                dataGridView.DataSource = new DataTable();
            }
        }

        public static void CarregarAlterarCabecalhoListaUtilXML(DataGridView dataGridView, Dictionary<string, string> dicionario, string pathFile)
        {
            if (File.Exists(pathFile))
            {
                foreach (KeyValuePair<string, string> par in dicionario)
                {
                    dataGridView.Columns[par.Key].HeaderText = par.Value;
                }
            }
            else
            {
                dataGridView.DataSource = new DataTable();
            }
        }

        /// <summary>
        /// Função que exclui o(s) nó(s) do arquivo xml
        /// </summary>
        /// <param name="cod"> Código/Id do nó filho </param>
        /// <param name="nomeArquivo"> Nome do arquivo xml </param>
        public static void ExcluirItemXmlUtl(string cod, string nomeCampoId, string nomeArquivo)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(XmlUtil.PathFileDbXml(nomeArquivo));

            XmlNode xmlExcluir = xml.SelectSingleNode("//Item["+ nomeCampoId + "='" + cod + "']");

            if (xmlExcluir != null)
            {
                xmlExcluir.ParentNode.RemoveChild(xmlExcluir);
                xml.Save(XmlUtil.PathFileDbXml(nomeArquivo));
            }
        }
    }
}
