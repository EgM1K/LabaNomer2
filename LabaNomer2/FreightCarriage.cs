﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public enum MaterialType
    {
        Wood = 1,
        Metal = 2,
        Coal = 3,
        Chemical = 4, // Хімікат
        Brittle = 5, // Крихкий матеріал
        Oil = 6, // Горючий матеріал
        Gas = 7, // Горючий матеріал
        Other = 8 // Будь-який інший матеріал
    }

    public class FreightCarriage : Carriage
    {
        public double Load { get; set; }
        public MaterialType Material { get; set; }
        public string OtherMaterial { get; set; }
        public double Weight { get; set; }
        

        public FreightCarriage(string id, double loadCapacity, MaterialType material)
            : base(id, "Freight", loadCapacity)
        {
            Load = 0;
            Material = material;
            OtherMaterial = "";
        }

        public void LoadCargo()
        {
            Console.WriteLine("Виберіть тип матеріалу:");
            foreach (var material in Enum.GetValues(typeof(MaterialType)))
            {
                Console.WriteLine($"{(int)material}. {material}");
            }
            MaterialType chosenMaterial = (MaterialType)Enum.Parse(typeof(MaterialType), Console.ReadLine());

            if (chosenMaterial == MaterialType.Other)
            {
                Console.WriteLine("Введіть назву матеріалу:");
                OtherMaterial = Console.ReadLine();
            }

            Console.WriteLine("Введіть вагу вантажу:");
            double cargoWeight = Convert.ToDouble(Console.ReadLine());

            double maxLoad = LoadCapacity;
            switch (chosenMaterial)
            {
                case MaterialType.Chemical:
                    maxLoad = 120;
                    break;
                case MaterialType.Oil:
                case MaterialType.Gas:
                    maxLoad = 80;
                    break;
                case MaterialType.Brittle:
                    maxLoad = 50;
                    break;
                case MaterialType.Wood:
                case MaterialType.Metal:
                case MaterialType.Coal:
                case MaterialType.Other:
                    maxLoad = 100;
                    break;
            }

            if (cargoWeight + Weight > maxLoad)
            {
                Console.WriteLine($"Вантаж не може перевищувати максимальну вантажопідйомність вагона для {chosenMaterial}.");
            }
            else
            {
                Load = cargoWeight + Weight;
                Material = chosenMaterial;
                Console.WriteLine($"Вантаж {Material} завантажено.");
            }
        }
            public void StationLoadCargo()
            {
                Console.Write("Введіть тип вантажу: ");
                MaterialType material = (MaterialType)Enum.Parse(typeof(MaterialType), Console.ReadLine());

                Console.Write("Введіть вагу вантажу: ");
                double cargoWeight = double.Parse(Console.ReadLine());

                if (Load == 0)
                {
                    double maxLoad = LoadCapacity;
                    switch (material)
                    {
                        case MaterialType.Chemical:
                            maxLoad = 120;
                            break;
                        case MaterialType.Oil:
                        case MaterialType.Gas:
                            maxLoad = 80;
                            break;
                        case MaterialType.Brittle:
                            maxLoad = 50;
                            break;
                        case MaterialType.Wood:
                        case MaterialType.Metal:
                        case MaterialType.Coal:
                        case MaterialType.Other:
                            maxLoad = 100;
                            break;
                        }

                    if (cargoWeight <= maxLoad)
                    {
                        Load = cargoWeight;
                        Material = material;
                        Console.WriteLine($"Вантаж {Material} завантажено.");
                    }
                    else
                    {
                        Console.WriteLine($"Вантаж не може перевищувати максимальну вантажопідйомність вагона для {material}.");
                    }
                }
                else
                {
                    Console.WriteLine("Вагон повинен бути порожнім перед завантаженням нового вантажу.");
                }
            }
        

            public void StationUnloadCargo()
            {
                Console.WriteLine($"Розвантажено вантаж вагою {Load}.");
                Load = 0;
                Material = MaterialType.Other;
                OtherMaterial = "";
            }
        }
    }

