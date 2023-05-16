using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.BusinessLogic
{
    public static class Helper
    {
        public static List<PathData> FindShortestPath(List<PathData> locations)
        {
            List<StorageLocation> unvisited = locations.Select(l => l.Location).ToList();
            List<StorageLocation> visited = new List<StorageLocation>();
            visited.Add(unvisited.First());
            unvisited.RemoveAt(0);

            while (unvisited.Count > 0)
            {
                StorageLocation currentLocation = visited.Last();
                StorageLocation nearestNeighbor = GetNearestNeighbor(currentLocation, unvisited);
                visited.Add(nearestNeighbor);
                unvisited.Remove(nearestNeighbor);
            }

            List<PathData> path = new List<PathData>();
            for (int i = 0; i <= visited.Count - 1; i++)
            {
                PathData data = locations.FirstOrDefault(l => l.Location == visited[i]);
                if (data != null)
                {
                    path.Add(data);
                }
            }
            return path;
        }
        private static StorageLocation GetNearestNeighbor(StorageLocation location, List<StorageLocation> neighbors)
        {
            double minDistance = double.MaxValue;
            StorageLocation nearestNeighbor = null;

            foreach (StorageLocation neighbor in neighbors)
            {
                double distance = GetDistance(location, neighbor);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestNeighbor = neighbor;
                }
            }
            return nearestNeighbor;
        }

        private static double GetDistance(StorageLocation location1, StorageLocation location2)
        {
            int dx = Math.Abs(location1.Row - location2.Row);
            int dy = Math.Abs(location1.Columnn - location2.Columnn);
            return dx + dy;
        }
    }
}
