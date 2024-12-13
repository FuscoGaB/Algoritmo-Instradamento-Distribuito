using System;

class RIPAlgorithm
{
    // Funzione per aggiornare la tabella di instradamento
    static bool UpdateRoutingTable(int[] router, int[][] allTables, int numRouters)
    {
        bool updated = false;

        // Per ogni destinazione nella tabella
        for (int dest = 0; dest < numRouters; dest++)
        {
            // Esamina i vicini e aggiorna le distanze
            for (int neighbor = 0; neighbor < numRouters; neighbor++)
            {
                if (allTables[neighbor][dest] < int.MaxValue) // Se il vicino ha una connessione a destinazione
                {
                    int newCost = allTables[neighbor][dest] + router[neighbor];
                    if (newCost < router[dest])
                    {
                        router[dest] = newCost;
                        updated = true;
                    }
                }
            }
        }
        return updated;
    }

    // Funzione per simulare l'algoritmo RIP
    static void SimulateRIP(int[][] routers, int numRouters, int maxIterations)
    {
        int iteration = 0;
        bool updated;

        // Continua per un numero massimo di iterazioni o finché non si raggiunge la convergenza
        while (iteration < maxIterations)
        {
            Console.WriteLine($"Iterazione {iteration + 1}:");
            // Mostra le tabelle di instradamento di tutti i router
            for (int i = 0; i < numRouters; i++)
            {
                Console.WriteLine($"Router {i}: [{string.Join(", ", routers[i])}]");
            }

            // Aggiorna tutte le tabelle di instradamento
            updated = false;
            for (int i = 0; i < numRouters; i++)
            {
                if (UpdateRoutingTable(routers[i], routers, numRouters))
                {
                    updated = true;
                }
            }

            // Se nessuna tabella è stata aggiornata, la rete è convergente
            if (!updated)
            {
                Console.WriteLine($"La rete è convergente dopo {iteration + 1} iterazioni.");
                break;
            }

            iteration++;
        }
    }

    static void Main(string[] args)
    {
        // Inizializzazione della rete con 4 router
        // Ogni router ha una tabella di distanze, dove:
        // - 0 per la stessa destinazione
        // - il costo del collegamento per i vicini diretti
        // - int.MaxValue (∞) per i collegamenti non diretti
        int[][] routers = new int[][]
        {
            new int[] { 0, 1, 4, int.MaxValue }, // Router A
            new int[] { 1, 0, 2, 5 },            // Router B
            new int[] { 4, 2, 0, 1 },            // Router C
            new int[] { int.MaxValue, 5, 1, 0 }  // Router D
        };

        int numRouters = 4;
        int maxIterations = 10;

        // Avvia l'algoritmo RIP
        SimulateRIP(routers, numRouters, maxIterations);
    }
}
