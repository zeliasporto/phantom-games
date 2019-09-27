using System;
using System.Collections.Generic;
using System.Text;

namespace ParserBanco
{
    public class ListaGeral
    {
        public Lista applist;
    }

    public class Lista
    {
        public Aplicativo[] apps;
    }

    public class Aplicativo
    {
        public long appid;
        public string name;

        public override string ToString() => name;
    }

    public class DetalhesAplicativo
    {
        public bool success;
        public DetalhesAplicativoData data;

        public override string ToString() => data.name;
    }

    public class DetalhesAplicativoData
    {
        public string type;
        public string name;
        public long steam_appid;
        public int required_age;
        public bool is_free;
        public string controller_support;
        public string detailed_description;
        public string about_the_game;
        public string short_description;
        public string supported_languages;
        public string header_image;
        public string website;
        public string legal_notice;
        public Categoria[] categories;
        public Categoria[] genres;

        public override string ToString() => name;
    }

    public class Categoria
    {
        public int id;
        public string description;

        public override string ToString() => description;
    }
}
